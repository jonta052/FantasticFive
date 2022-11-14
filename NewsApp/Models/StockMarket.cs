using System.Text.Json.Serialization;

namespace NewsApp.Models
{
    public class StockMarket
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("close")]
        public double Close { get; set; }


        [JsonPropertyName("percentChange")]
        public double PercentChange { get; set; }

        
    }
    public class TopThree
    {
        [JsonPropertyName("top3")]
        public IEnumerable<StockMarket> Top3 { get; set; }
        //public bool Empty
        //{
        //    get
        //    {
        //        return Top3 == null;
        //    }
        //}
        //public bool Empty([Optional] double? close, [Optional] double? percentChange, string Name)
        //{
        //    if (close == null || percentChange == null || Name == null)
        //    {
        //        return Top3 == null;
        //    }
        //    else
        //    {
        //        return Top3 != null;
        //    }
        //}
    }

    
}
