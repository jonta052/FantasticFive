﻿using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;

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
            var summary = await result.Content.ReadFromJsonAsync<TopThree>();
            return View("Index", summary); 
              
        }
    }
}
