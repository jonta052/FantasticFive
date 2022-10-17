using NewsApp.Models;
using System.Security.Claims;

namespace NewsApp.Services
{
    public interface ISubscriptionService
    {
        Subscription CreateSubscription(int subscriptionId, User user, KlarnaOrder klarnaOrder, double totalAmount);

        bool HasSubscription(ClaimsPrincipal user);
    }
}
