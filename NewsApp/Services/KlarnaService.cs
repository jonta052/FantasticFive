using Microsoft.AspNetCore.Identity;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Models.Klarna;

namespace NewsApp.Services
{
    public class KlarnaService : IKlarnaService
    {
        private ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpOrderClient;
        private readonly ISubscriptionService _subscriptionService;

        public KlarnaService(IHttpClientFactory httpClientFactory, ApplicationDbContext db, UserManager<User> userManager, ISubscriptionService subService)
        {
            _httpClient = httpClientFactory.CreateClient("klarna-payment");
            _httpOrderClient = httpClientFactory.CreateClient("klarna-order");
            _db = db;
            _userManager = userManager;
            _subscriptionService = subService;
        }

        public async Task<KlarnaSessionResult> CreateSession(int subscriptionType)
        {
            var orderLines = new List<KlarnaSessionRequest.OrderLine> { GetSubscriptionOrderLine(subscriptionType) };
            var klarnaSessionRequest = GetKlarnaSessionRequest(orderLines);

            var response = await _httpClient.PostAsJsonAsync("sessions", klarnaSessionRequest);

            if (!response.IsSuccessStatusCode) return null;

            var data = await response.Content.ReadFromJsonAsync<KlarnaSessionResponse>();
            return new KlarnaSessionResult
            {
                OrderLines = orderLines,
                SessionResponse = data
            };
        }

        public async Task<KlarnaOrder> CreateOrder(string authorizationToken, string userId, IEnumerable<KlarnaSessionRequest.OrderLine> orderLines)
        {
            var user = await _userManager.FindByIdAsync(userId);

            
            var order = new KlarnaOrderRequest
            {
                TotalAmount = orderLines.Sum(ol => ol.TotalAmount),
                TotalTaxAmount = orderLines.Sum(ol => ol.TotalTaxAmount),
                PurchaseCountry = "SE",
                PurchaseCurrency = "SEK",
                OrderLines = orderLines,
            };

            var res = await _httpClient.PostAsJsonAsync($"authorizations/{authorizationToken}/order", order);

            if (!res.IsSuccessStatusCode || user == null) return null;

            var data = await res.Content.ReadFromJsonAsync<KlarnaOrderResponse>();

            var klarnaOrder = new KlarnaOrder
            {
                FraudStatus = data.FraudStatus,
                KlarnaOrderId = data.OrderId,
                NumberOfDays = data.AuthorizationPaymentMethod.NumberOfDays,
                NumberOfInstallments = data.AuthorizationPaymentMethod.NumberOfInstallments,
                Type = data.AuthorizationPaymentMethod.Type,
                RedirectUrl = data.RedirectUrl,
                User = user,
            };

            var successfulOrderCapture = await CaptureOrder(klarnaOrder, order.TotalAmount);
            if (!successfulOrderCapture) return null;

            
            _db.Add(klarnaOrder);
            await _db.SaveChangesAsync();

            //Own code
            _subscriptionService.CreateSubscription(1, user, klarnaOrder, order.TotalAmount);

            return klarnaOrder;
        }

        private async Task<bool> CaptureOrder(KlarnaOrder order, long orderTotalAmount)
        {
            var captureOrderRequest = new KlarnaCaptureOrderRequest
            {
                CapturedAmount = orderTotalAmount
            };
            var res = await _httpOrderClient.PostAsJsonAsync($"orders/{order.KlarnaOrderId}/captures", captureOrderRequest);

            return res.IsSuccessStatusCode;
        }

        private static KlarnaSessionRequest GetKlarnaSessionRequest(IEnumerable<KlarnaSessionRequest.OrderLine> orderLines)
        {
            return new KlarnaSessionRequest
            {
                Locale = "en-GB",
                PurchaseCountry = "SE",
                PurchaseCurrenct = "SEK",
                OrderLines = orderLines,
                OrderAmount = orderLines.Sum(ol => ol.TotalAmount),
                OrderTaxAmount = orderLines.Sum(ol => ol.TotalTaxAmount),
            };
        }

        private KlarnaSessionRequest.OrderLine GetSubscriptionOrderLine(int subscriptionTypeId)
        {
            var subType = _db.SubscriptionTypes.Find(subscriptionTypeId);
            var price = GetKlarnaPrice((double)subType.Price); // Calculate price base of the SubscriptionType
            return new KlarnaSessionRequest.OrderLine
            {
                Name = $"Subscription {subType.TypeName}",
                Type = "digital",
                TotalAmount = price,
                UnitPrice = price,
                Quantity = 1,
            };
        }

        private static long GetKlarnaPrice(double price) => (long)Math.Round(price, 2) * 100;
    }
}
