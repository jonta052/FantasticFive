using System.Text.Json.Serialization;

namespace NewsApp.Models.Klarna
{
    public class KlarnaOrderRequest
    {
        [JsonPropertyName("order_amount")]
        public long TotalAmount { get; set; }

        [JsonPropertyName("order_tax_amount")]
        public long TotalTaxAmount { get; set; }

        [JsonPropertyName("purchase_country")]
        public string PurchaseCountry { get; set; }

        [JsonPropertyName("purchase_currency")]
        public string PurchaseCurrency { get; set; }

        [JsonPropertyName("order_lines")]
        public IEnumerable<KlarnaSessionRequest.OrderLine> OrderLines { get; set; }
    }

    public class AuthorizationPaymentMethod
    {
        [JsonPropertyName("number_of_days")]
        public int NumberOfDays { get; set; }

        [JsonPropertyName("number_of_installments")]
        public int NumberOfInstallments { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class KlarnaOrderResponse
    {
        [JsonPropertyName("authorized_payment_method")]
        public AuthorizationPaymentMethod AuthorizationPaymentMethod { get; set; }

        [JsonPropertyName("fraud_status")]
        public string FraudStatus { get; set; }

        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        [JsonPropertyName("redirect_url")]
        public string RedirectUrl { get; set; }
    }

    public class KlarnaCaptureOrderRequest
    {
        [JsonPropertyName("captured_amount")]
        public long CapturedAmount { get; set; }

    }
}
