﻿using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
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


        public ArticleController(IArticleService articleService, ApplicationDbContext db)
        {
            _articleService = articleService;
            _db = db;
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
            return View(article);
        }

        // GET: ArticleController/Create
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

       public IActionResult CategoryIndex(string CategoryName)
        {
            //Get category from category name
            var category = _db.Categories.Where(c => c.Name == CategoryName).FirstOrDefault();
            //Get articles belonging to that category
            var catagoryArticles = from a in _db.Articles where a.CategoryId == category.Id select a;
            return View(catagoryArticles);
        }

    }
}
