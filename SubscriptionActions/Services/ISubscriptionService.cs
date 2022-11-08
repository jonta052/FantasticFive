using Microsoft.AspNetCore.Mvc;

namespace SubscriptionActions.Services
{
    public interface ISubscriptionService
    {
        void SetExpiredSubscription();
        Task<IActionResult> ReminderEmail();
        Task<IActionResult> SubscriberEmail();
    }
}
