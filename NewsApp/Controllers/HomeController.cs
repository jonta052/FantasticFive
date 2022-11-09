using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsApp.Data;
using NewsApp.Models;
using System.Diagnostics;
using NuGet.Protocol;
using Newtonsoft.Json.Linq;
using NewsApp.Models.Email;
using NewsApp.Services;

namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly HttpClient _httpClient;
        private readonly HttpClient _stockMarket;
        private readonly HttpClient _userLocationInfo;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db,
            IHttpClientFactory httpClientFactory, IHttpClientFactory stockMarket, IHttpClientFactory userLocationInfo,
            IEmailService emailService)
        {
            _logger = logger;
            _db = db;
            _httpClient = httpClientFactory.CreateClient("weatherForecast");
            _stockMarket = stockMarket.CreateClient("stockMarket");
            _userLocationInfo = userLocationInfo.CreateClient("getUserLocationInfo");
            _emailService = emailService;


        }

        public IActionResult Index(string CategoryName)
        {
            if (string.IsNullOrEmpty(CategoryName))
            {
            var popular = _db.Articles
              .OrderByDescending(m => m.Likes.Count)
              .Take(5).ToList();
                return View(popular);
            }
            //Get category from category name
            var category = _db.Categories.Where(c => c.Name == CategoryName).FirstOrDefault();
            //Get articles belonging to that category
            var catagoryArticles = from a in _db.Articles where a.CategoryId == category.Id select a;

            return View(catagoryArticles);

        }

        public IActionResult PopularArticles()
        {
            var popular = _db.Articles
              .OrderByDescending(m => m.Likes.Count)
              .Take(5).ToList();
            return PartialView("~/Home/PopularArticles", popular);
        }

        public IActionResult LatestArticles()
        {
            var latest = _db.Articles
              .OrderByDescending(m => m.DateStamp)
              .FirstOrDefault();
            return View(latest);
        }
        public IActionResult EditorsChoice()
        {

            //Get articles belonging to that category
            var editorsArticles = from a in _db.Articles where a.EditorChoice == true select a;
            return PartialView("~/Home/EditorsChoice", editorsArticles);
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
            var request = HttpContext.Connection.RemoteIpAddress.ToString();
            //https://ipinfo.io/213.80.110.182?token=bde75ceacf2669
            
            var userInfo = await _userLocationInfo.GetAsync($"{request}?token=bde75ceacf2669");
            var data = await userInfo.Content.ReadAsStringAsync();
            dynamic info = JObject.Parse(data);
            string city = info.city;
            /*if (string.IsNullOrEmpty(city))
        {
            return View();
        }*/
            if (string.IsNullOrEmpty(city))
            {
                var result = await _httpClient.GetAsync($"Forecast?city=Linköping");
                if (result.IsSuccessStatusCode)
                {
                    var forecast = await result.Content.ReadFromJsonAsync<WeatherForecast>();
                    //ViewBag.result = body;
                    return View(forecast);
                }
            }
            else
            {
                var result = await _httpClient.GetAsync($"Forecast?city={city}");
                if (result.IsSuccessStatusCode)
                {
                    var forecast = await result.Content.ReadFromJsonAsync<WeatherForecast>();
                    //ViewBag.result = body;
                    return View(forecast);
                }
            }
            //var body = await result.Content.ReadAsStringAsync();

            
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

        public IActionResult SearchNews()
        {
            /*var temp = (from a in _db.Articles where a.Content.Contains(search) select a).ToList();
            TempData["SelectedArticles"] = temp;*/
            
            return View();
        }

        [HttpGet]
        public ViewResult Edit(string id)
        {

            var user = _db.Users.Where(u => u.Id == id).FirstOrDefault();
            User uzer = new User();
            uzer.Id = user.Id;
            uzer.FirstName = user.FirstName;
            uzer.Email = user.Email;

            return View(uzer);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Detail()
        {
            return View();
        }
        

    }
}