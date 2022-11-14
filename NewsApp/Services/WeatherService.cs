using Microsoft.IdentityModel.Protocols;
using NewsApp.Data;
using NewsApp.Models;
using Newtonsoft.Json.Linq;

namespace NewsApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ApplicationDbContext _db;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _userLocationInfo;

        public WeatherService(ApplicationDbContext db, IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpClient = httpClientFactory.CreateClient("weatherForecast");
            _httpContextAccessor = httpContextAccessor;
            _userLocationInfo = httpClientFactory.CreateClient("getUserLocationInfo");
        }

        public async Task<WeatherForecast> WeatherApp()
        {
            WeatherForecast forecasts = new WeatherForecast();
            forecasts.Summary = "Cloudy";
            forecasts.City = "ERROR";
            forecasts.Lang = "sv";
            forecasts.TemperatureF = 80;
            forecasts.TemperatureC = 404;
            forecasts.Humidity = 50;
            forecasts.WindSpeed = 3;
            forecasts.Date = DateTime.Now;

            //var request = HelperIp.GetRemoteIPAddress(_httpContextAccessor.HttpContext).ToString();

            var request = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            //https://ipinfo.io/213.80.110.182?token=bde75ceacf2669

            string city = "";
            //Ip is not local IP
            if (request.Length > 8)
            {
                var userInfo = await _userLocationInfo.GetAsync($"{request}?token=bde75ceacf2669");
                var data = await userInfo.Content.ReadAsStringAsync();
                dynamic info = JObject.Parse(data);
                city = info.city;
            }
            //Set City to Linköping as default if ip is local ip
            else
            {
                city = "Linköping";
            }

            var result = await _httpClient.GetAsync($"Forecast?city={city}");
            //Weatherpage is up and running
            if (result.IsSuccessStatusCode)
            {
                var forecast = await result.Content.ReadFromJsonAsync<WeatherForecast>();
                
                return forecast;
            }
            //Weatherpage is down
            else
            {
                return forecasts;
            }
            
        }


    }
}
