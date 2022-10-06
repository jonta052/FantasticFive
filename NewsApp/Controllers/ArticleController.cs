using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;


        public ArticleController(IArticleService articleService, ApplicationDbContext db, UserManager<User> userManager)
        {
            _articleService = articleService;
            _db = db;
            _userManager = userManager;
        }


        // GET: Article
        public IActionResult Index(string search)
        {
            var selectedArticles = (from a in _db.Articles where a.Content.Contains(search) || a.Title.Contains(search) select a).ToList();

            if (selectedArticles.IsNullOrEmpty())
            {
                selectedArticles = _articleService.GetArticles().ToList();
            }

            return View(selectedArticles);
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

        [Authorize(Roles = $"{Roles.Administrator},{Roles.Editor}")]
        // GET: ArticleController/Create
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Editor}")]
        public IActionResult Create()
        {
            var categories = _db.Categories.ToList();
            var selectList = new SelectList(categories, "Id", "Name");

            ViewBag.CategoryName = selectList;
            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Editor}")]
        public IActionResult Create(Article article)
        {
            var categories = _db.Categories.ToList();
            var selectList = new SelectList(categories, "Id", "Name");

            ViewBag.CategoryName = selectList;
            try
            {
                _articleService.CreateArticle(article);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(article);
            }
        }

        // GET: ArticleController/Edit/5
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Editor}")]
        public IActionResult Edit(int id)
        {
            var article = _articleService.GetArticle(id);
            return View(article);
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Editor}")]
        public IActionResult Edit(int id, Article article)
        {
            try
            {
                _articleService.UpdateArticle(article);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(article);
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(article);
            }
        }

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
