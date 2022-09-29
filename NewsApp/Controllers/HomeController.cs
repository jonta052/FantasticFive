using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsApp.Data;
using NewsApp.Models;
using System.Diagnostics;

namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly HttpClient _httpClient;
        private readonly HttpClient _stockMarket;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IHttpClientFactory httpClientFactory, IHttpClientFactory stockMarket)
        {
            _logger = logger;
            _db = db;
            _httpClient = httpClientFactory.CreateClient("weatherForecast");
            _stockMarket = stockMarket.CreateClient("stockMarket");
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

        public async Task <IActionResult> WeatherApp(/*string city*/)
        {
            /*if (string.IsNullOrEmpty(city))
            {
                return View();
            }*/
            var result = await _httpClient.GetAsync($"Forecast?city=Linköping");
            //var body = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var forecast = await result.Content.ReadFromJsonAsync<WeatherForecast>();
                //ViewBag.result = body;
                return View(forecast);
            }
            return View();
        }

        public async Task<IActionResult> StockMarket(/*string city*/)
        {
            var result = await _stockMarket.GetAsync("summary");
        

            if (result.IsSuccessStatusCode)
            {
                var summary = await result.Content.ReadFromJsonAsync<TopThree>();
                
                return View(summary);
            }
            return View();
        }

    }
}