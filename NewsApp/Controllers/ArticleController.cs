using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Services;
using X.PagedList;

namespace NewsApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public ArticleController(IArticleService articleService, ApplicationDbContext db, UserManager<User> userManager, IConfiguration configuration)
        {
            _articleService = articleService;
            _db = db;
            _userManager = userManager;
            _configuration = configuration;
        }



        //[HttpPost(nameof(UploadFile))]
        //public async Task<IActionResult> UploadFile(CreateArticleVM files)
        //{

        //    string systemFileName = files.File.FileName;
        //    systemFileName = _db.Articles.FirstOrDefault().Id+ "_"+systemFileName;

        //    string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");
        //    // Retrieve storage account from connection string.    
        //    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
        //    // Create the blob client.    
        //    CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
        //    // Retrieve a reference to a container.    
        //    CloudBlobContainer container = blobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerName"));
        //    // This also does not make a service call; it only creates a local object.    
        //    CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
        //    await using (var data = files.File.OpenReadStream())
        //    {
        //        await blockBlob.UploadFromStreamAsync(data);
        //    }
        //    var uri = blockBlob.Uri.ToString();
        //    _db.SaveChanges();
        //    return View();
        //}

        
        public IActionResult Index(int? page)
        {
            if (page != null)
            {
                TempData["JumpToOpinions"] = "hej";
            }
            
            var allArticles = _articleService.GetOneArticleForCategories();
            //If no page number given, show first page
            var pageNumber = page ?? 1;
            ViewBag.oneArticlePage = allArticles.ToPagedList(pageNumber, 4);

            return View(allArticles.ToPagedList(pageNumber, 4));
        }

        public IActionResult SearchArticles(string search, int? page)
        {
            ViewBag.search = search;
            var selectedArticles = (from a in _db.Articles where a.Content.Contains(search) || a.Title.Contains(search) select a).ToList();
            //If no page number given, show first page
            var pageNumber = page ?? 1;
            ViewBag.oneArticlePage = selectedArticles.ToPagedList(pageNumber, 4);

            if (selectedArticles.IsNullOrEmpty())
            {
                return View("NotFound");
            }

            return View(selectedArticles.ToPagedList(pageNumber, 4));
        }

        // GET: ArticleController/Details/5
        public IActionResult Details(int id)
        {
            var article = _articleService.GetArticle(id);
            var click = _db.Articles.Where(c => c.Id == id).FirstOrDefault();
            click.Views += 1;
            _db.Update(click);
            _db.SaveChanges();
            return View(article);
        }

        
        // GET: ArticleController/Create
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Editor}")]
        public IActionResult Create()
        {
            var categories = _db.Categories.ToList();
            var selectList = new SelectList(categories, "Id", "Name");

            //_articleService.UploadToBlob()
            ViewBag.CategoryName = selectList;
            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Editor}")]
        public async Task<IActionResult> Create(CreateArticleVM articleVM)
        {
            var categories = _db.Categories.ToList();
            var selectList = new SelectList(categories, "Id", "Name");
            //DialogResult result = openFileDialog1.ShowDialog();
            ViewBag.CategoryName = selectList;

            string systemFileName = articleVM.File.FileName;
            systemFileName = _db.Articles.Select(a => a.Id).OrderByDescending(x=>x).FirstOrDefault()+1 + "_" + systemFileName;

            string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");
            // Retrieve storage account from connection string.    
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            // Create the blob client.    
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container.    
            CloudBlobContainer container = blobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerName"));
            // This also does not make a service call; it only creates a local object.    
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
            await using (var data = articleVM.File.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(data);
            }
            var uri = blockBlob.Uri.ToString();
            articleVM.Article.ImageLink = uri;

            try
            {
                _articleService.CreateArticle(articleVM.Article);
                return RedirectToAction("Index","Editor");
            }
            catch
            {
                //ViewBag.NoCategory = "You need to choose a category";
                ModelState.AddModelError("CategoryId", "Category already exists");
                return View(articleVM);
            }

        }
        // GET: ArticleController/Edit/5
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Editor}")]
        public IActionResult Edit(int id)
        {
            var categories = _db.Categories.ToList();
            var selectList = new SelectList(categories, "Id", "Name");
            ViewBag.CategoryName = selectList;

            var article = _articleService.GetArticle(id);
            CreateArticleVM articleVm = new CreateArticleVM();
            articleVm.Article = article;
            
            return View(articleVm);
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Editor}")]
        public async Task<IActionResult> Edit(CreateArticleVM articleVm)
        {
            var categories = _db.Categories.ToList();
            var selectList = new SelectList(categories, "Id", "Name");
            ViewBag.CategoryName = selectList;

            if (articleVm.File != null)
            {
                string systemFileName = articleVm.File.FileName;
                systemFileName = _db.Articles.Select(a => a.Id).OrderByDescending(x => x).FirstOrDefault() + 1 + "_" + systemFileName;

                string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");
                // Retrieve storage account from connection string.    
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                // Create the blob client.    
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                // Retrieve a reference to a container.    
                CloudBlobContainer container = blobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerName"));
                // This also does not make a service call; it only creates a local object.    
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
                await using (var data = articleVm.File.OpenReadStream())
                {
                    await blockBlob.UploadFromStreamAsync(data);
                }
                var uri = blockBlob.Uri.ToString();

                //Only change old imagelink if uri contains something
                if (!string.IsNullOrEmpty(uri))
                {
                    articleVm.Article.ImageLink = uri;
                }
            }
            
            try
            {
                if (ModelState.IsValid)
                {
                    _articleService.UpdateArticle(articleVm.Article);
                }

                return RedirectToAction("Index", "Editor");
                //return RedirectToAction("Details", new { id = articleVm.Article.Id });
                
            }
            catch
            {
                ModelState.AddModelError("CategoryId", "Category already exists");
                return View(articleVm);
            }
        }

        // GET: ArticleController/Delete/5
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Editor}")]
        public ActionResult Delete(int id)
        {
            var article = _articleService.GetArticle(id);
            return View(article);
        }

        // POST: ArticleController/Delete/5
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Editor}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Article article)
        {
            try
            {
                _articleService.DeleteArticle(id);
                return RedirectToAction("Index", "Editor");
            }
            catch
            {
                return View(article);
            }
        }

        //Unneccesary
       public IActionResult CategoryIndex(string CategoryName)

        {
            //Get category from category name
            var category = _db.Categories.Where(c => c.Name == CategoryName).FirstOrDefault();
            //Get articles belonging to that category
            var catagoryArticles = from a in _db.Articles where a.CategoryId == category.Id select a;
            return View(catagoryArticles);
        }
        
        public IActionResult ClickLike(int like, int dislike)
        {
            TempData["JumpToOpinions"] = "hej";
            var currentClick = 0;
            if (like > dislike)
            {
                currentClick = like;
            }
            else
            {
                currentClick = dislike;
            }

            var userId = _userManager.GetUserId(User);
            var existingLike = _db.Likes.FirstOrDefault(l => l.ArticleId == currentClick && l.UserId == userId);
            var existingDislike = _db.Dislikes.FirstOrDefault(l => l.ArticleId == currentClick && l.UserId == userId);
            if (like > dislike)
            {

                //user has liked article before
                if (existingLike != null)
                {

                    _db.Remove(existingLike);
                    _db.SaveChanges();
                    return RedirectToAction("Details", new { id = like });
                }
                //user has not disliked article before
                else if (existingDislike == null)
                {
                    var userLike = new Like
                    {
                        UserId = userId,
                        ArticleId = like
                    };

                    _db.Add(userLike);
                    _db.SaveChanges();
                }
                else if(existingDislike != null)
                {
                    var userLike = new Like
                    {
                        UserId = userId,
                        ArticleId = like
                    };

                    _db.Remove(existingDislike);
                    _db.Add(userLike);
                    _db.SaveChanges();
                };


                return RedirectToAction("Details", new { id = like });
            }
            else
            {


                if (existingDislike != null)
                {
                    // TODO Remove like
                    _db.Remove(existingDislike);
                    _db.SaveChanges();
                    return RedirectToAction("Details", new { id = dislike });
                }
                else if (existingLike == null)
                {
                    var userDislike = new Dislike
                    {
                        UserId = userId,
                        ArticleId = dislike
                    };

                    _db.Add(userDislike);
                    _db.SaveChanges();
                }
                else if (existingLike != null)
                {
                    var userDislike = new Dislike
                    {
                        UserId = userId,
                        ArticleId = dislike
                    };

                    _db.Remove(existingLike);
                    _db.Add(userDislike);
                    _db.SaveChanges();
                };
                return RedirectToAction("Details", new { id = dislike });

            }


        }

    }
}
