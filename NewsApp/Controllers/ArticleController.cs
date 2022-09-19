﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
      
        private readonly ApplicationDbContext _db;


        public ArticleController(IArticleService articleService, ApplicationDbContext db)
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

        // GET: ArticleController/Details/5
        public IActionResult Details(int id)
        {
            var articles = _articleService.GetArticle(id);
            return View(articles);
        }

        // GET: ArticleController/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Article article)
        {
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
        public IActionResult Edit(int id)
        {
            var article = _articleService.GetArticle(id);
            return View(article);
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Delete(int id)
        {
            var article = _articleService.GetArticle(id);
            return View(article);
        }

        // POST: ArticleController/Delete/5
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

       public IActionResult CategoryIndex(string Id)
        {
            int categoryIdInt = Convert.ToInt32(Id);
            var category = _db.Categories.Find(categoryIdInt);
            var catagoryArticles = from a in _db.Articles where a.CategoryId == categoryIdInt select a;
            return View(catagoryArticles);
        }

    }
}
