﻿using System.Text.Json.Serialization;

namespace NewsApp.Models
{
    public class StockMarket
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("close")]
        public double Close { get; set; }
        [JsonPropertyName("prevClose")]
        public double PrevClose { get; set; }
        [JsonPropertyName("percentChange")]
        public double PercentChange { get; set; }
    }
    public class TopThree
    {
        [JsonPropertyName("top3")]
        public IEnumerable<StockMarket> Top3 { get; set; }
    }
}