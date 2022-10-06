using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ApplicationDbContext _db;
        private readonly HttpClient _httpClient;

        public WeatherService(ApplicationDbContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _httpClient = httpClientFactory.CreateClient("weatherForecast");
        }


        public async Task<WeatherForecast> WeatherApp(/*string city*/)
        {
            WeatherForecast forecasts = new WeatherForecast();
            forecasts.Summary = "Cloudy";
            forecasts.City = "Linköping";
            forecasts.Lang = "sv";
            forecasts.TemperatureF = 80;
            forecasts.TemperatureC = 15;
            forecasts.Humidity = 50;
            forecasts.WindSpeed = 3;
            forecasts.Date = DateTime.Now;

            var result = await _httpClient.GetAsync($"Forecast?city=Linköping");
            
            if (result.IsSuccessStatusCode)
            {
                var forecast = await result.Content.ReadFromJsonAsync<WeatherForecast>();
                return forecast;
            }
            return forecasts;
            
        }


    }
}
