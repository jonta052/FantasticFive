using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using NewsApp.Models;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace NewsApp.ViewComponents
{
    public class StockViewComponent : ViewComponent
    {

        private readonly HttpClient _stockMarket;

        public StockViewComponent(IHttpClientFactory httpClientFactory)
        {
            _stockMarket = httpClientFactory.CreateClient("stockMarket"); 
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result =_stockMarket.GetAsync("summary").Result;
            TopThree topThree = new TopThree();

            if (result.IsSuccessStatusCode)
            {
                topThree = await result.Content.ReadFromJsonAsync<TopThree>();
            }
            
            return View("Index", topThree); 
              
        }
    }
}
