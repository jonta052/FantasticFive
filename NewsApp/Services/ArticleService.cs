using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Services
{
    //ctrl + "." implement interface
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public ArticleService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public Article CreateArticle(Article article)
        {
            _db.Articles.Add(article);
            _db.SaveChanges();
            return article;
        }

      
        public async void DeleteArticle(int id)
        {

            var article = GetArticle(id);
            _db.Remove(article);
            _db.SaveChanges();
            var fileName = article.ImageLink.Substring(article.ImageLink.LastIndexOf('/') + 1);

            string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = _configuration.GetValue<string>("BlobContainerName");
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
            var blob = cloudBlobContainer.GetBlobReference(fileName);
            await blob.DeleteIfExistsAsync();

            

            
        }

        public Article GetArticle(int id)
        {
            //var article = _db.Articles.Find(id);
            //_db.Entry(article).Collection(a => a.Comments).Load();

            /*var article = _db.Articles.Include(a => a.Comments)
                .FirstOrDefault(a => a.Id == id);*/

            //Use any of the two methods above or install
            //lazy loading from nuget packet manager,
            //don't forget to add virtual in models
            var article = _db.Articles.Find(id);
            return article;
            
        }

        public IEnumerable<Article> GetArticles()
        {
            return _db.Articles.ToList();
        }
        public Article UpdateArticle(Article article)
        {
            _db.Update(article);
            _db.SaveChanges();
            return article;
        }


        public IEnumerable<Article> PopularArticles()
        {
            var popularArticles = _db.Articles
               .OrderByDescending(m => m.Likes.Count);

            return popularArticles;
        }

        public IEnumerable<Article> LatestArticles()
        {
            var latestArticles = _db.Articles
                  .OrderByDescending(m => m.DateStamp);

            return latestArticles;  
        }
        public IEnumerable<Article> EditorsChoice()
        {
            var editorsArticles = from a in _db.Articles where a.EditorChoice == true select a;
            

            return editorsArticles;
        }
        public IEnumerable<Article> GetOneArticleForCategories()
        {
            var categories = _db.Categories.ToList();
            var result = new List<Article>();
            foreach (var item in categories)
            {
                var article = item.Articles.OrderByDescending(a => a.DateStamp).FirstOrDefault();
                if (article != null)
                {
                    result.Add(article);
                }
            }
            return result;
        }



        //public BlobContainerClient InitBlobService()
        //{
        //    BlobServiceClient blobServiceClient = new BlobServiceClient("AzureWebJobsStorage");
        //    string containerName = "blobimages";
        //    return blobServiceClient.GetBlobContainerClient(containerName);
        //}
        //public async void UploadToBlob(IFormFile file)
        //{
        //    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image/Article-Images");
        //    string fileNameWithPath = Path.Combine(path, file.FileName);

        //    BlobClient blobClient = InitBlobService().GetBlobClient(fileNameWithPath);

        //    await blobClient.UploadAsync(fileNameWithPath, true);
        //    await blobClient.UploadAsync(file.OpenReadStream(), true);
        //}
        //public Uri GetBlobImage(string imageUrl)
        //{
        //    var uri = InitBlobService().GetBlobClient(imageUrl).Uri;
        //    return uri;
        //}

    }
}
 