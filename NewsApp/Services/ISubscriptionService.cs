using NewsApp.Models;

namespace NewsApp.Services
{
    public interface ISubscriptionService
    {
        Subscription CreateSubscription(int subscriptionId, User user, KlarnaOrder klarnaOrder, double totalAmount);
    }
}
