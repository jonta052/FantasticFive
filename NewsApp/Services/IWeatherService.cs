using NewsApp.Models;
namespace NewsApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecast> WeatherApp();
    }
}
