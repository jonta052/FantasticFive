using NewsApp.Models;
using NewsApp.Models.Klarna;

namespace NewsApp.Services
{
    public interface IKlarnaService
    {
        Task<KlarnaSessionResult> CreateSession(int subscriptionType);

        Task<KlarnaOrder> CreateOrder(string authorizationToken, string userId, IEnumerable<KlarnaSessionRequest.OrderLine> orderLines);
    }
}
