using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Services
{
    //ctrl + "." implement interface
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;

        public ArticleService(ApplicationDbContext db)
        {
            _db = db;
        }
        public Article CreateArticle(Article article)
        {
            _db.Articles.Add(article);
            _db.SaveChanges();
            return article;
        }

      
        public void DeleteArticle(int id)
        {
            var article = GetArticle(id);
            _db.Remove(article);
            _db.SaveChanges();
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
    }
}
