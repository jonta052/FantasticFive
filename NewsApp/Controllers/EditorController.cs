using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Services;
using System.Data;

namespace NewsApp.Controllers
{
    [Authorize(Roles = $"{Roles.Administrator},{Roles.Editor}")]
    public class EditorController : Controller
    {
        private readonly IArticleService _articleService;

        private readonly ApplicationDbContext _db;


        public EditorController(IArticleService articleService, ApplicationDbContext db)
        {
            _articleService = articleService;
            _db = db;
        }


        // GET: Article
        
        public IActionResult Index()
        {

            var articles = _articleService.GetArticles();



            return View(articles);
        }

        
        
        public IActionResult EditorChoice(int id)
        {
            var article = _articleService.GetArticle(id);
            try
            {
                if (article.EditorChoice == true)
                {
                    article.EditorChoice = false;
                    _db.Update(article);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    article.EditorChoice = true;
                    _db.Update(article);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}
