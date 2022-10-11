using System.Text.Json.Serialization;

namespace NewsApp.Models.Klarna
{
    public class KlarnaSessionRequest
    {
        public class OrderLine
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("reference")]
            public string Reference { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("quantity")]
            public long Quantity { get; set; }

            [JsonPropertyName("unit_price")]
            public long UnitPrice { get; set; }

            [JsonPropertyName("tax_rate")]
            public long TaxRate { get; set; }

            [JsonPropertyName("total_amount")]
            public long TotalAmount { get; set; }

            [JsonPropertyName("total_discount_amount")]
            public long TotalDiscountAmount { get; set; }

            [JsonPropertyName("total_tax_amount")]
            public long TotalTaxAmount { get; set; }

            [JsonPropertyName("image_url")]
            public string ImageUrl { get; set; }

            [JsonPropertyName("product_url")]
            public string ProductUrl { get; set; }
        }

        [JsonPropertyName("purchase_country")]
        public string PurchaseCountry { get; set; }

        [JsonPropertyName("purchase_currency")]
        public string PurchaseCurrenct { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("order_amount")]
        public long OrderAmount { get; set; }

        [JsonPropertyName("order_tax_amount")]
        public long OrderTaxAmount { get; set; }

        [JsonPropertyName("order_lines")]
        public IEnumerable<OrderLine> OrderLines { get; set; }
    }

    public class KlarnaSessionResponse
    {
        public class AssetUrls
        {
            [JsonPropertyName("descriptive")]
            public string Descriptive { get; set; }

            [JsonPropertyName("standard")]
            public string Standard { get; set; }
        }

        public class PaymentMethodCategory
        {
            [JsonPropertyName("identifier")]
            public string Identifier { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("asset_urls")]
            public AssetUrls AssetUrls { get; set; }
        }

        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }

        [JsonPropertyName("client_token")]
        public string ClientToken { get; set; }

        [JsonPropertyName("payment_method_categories")]
        public IEnumerable<PaymentMethodCategory> PaymentMethodCategories { get; set; }
    }

    public class KlarnaSessionResult
    {
        public KlarnaSessionResponse SessionResponse { get; set; }

        public IEnumerable<KlarnaSessionRequest.OrderLine> OrderLines { get; set; }
    }
}
