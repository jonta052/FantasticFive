using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Models;
using System.Diagnostics;

namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PopularArticles()
        {
            var popular = _db.Articles
              .OrderByDescending(m => m.Likes)
              .Take(5).ToList();
            return View(popular);
        }

        public IActionResult LatestArticles()
        {
            var latest = _db.Articles
              .OrderByDescending(m => m.DateStamp)
              .FirstOrDefault();
            return View(latest);
        }

        public IActionResult EditorChoice()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}