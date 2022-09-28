﻿using System.Text.Json.Serialization;

namespace NewsApp.Models
{
    public class WeatherForecast
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("lang")]
        public string Lang { get; set; }
        [JsonPropertyName("temperatureF")]
        public int TemperatureF { get; set; }
        [JsonPropertyName("temperatureC")]
        public int TemperatureC { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
        [JsonPropertyName("windSpeed")]
        public int WindSpeed { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

    }
}
