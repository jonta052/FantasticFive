using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
namespace NewsApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecast> WeatherApp();
    }
}
