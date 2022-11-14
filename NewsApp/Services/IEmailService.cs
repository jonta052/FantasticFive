using NewsApp.Models.Email;

namespace NewsApp.Services
{
    public interface IEmailService
    {
        Task<string> SendEmail(SubscriptionEmail newEmail);
        
    }
}
