using NewsApp.Models.Email;
using NewsApp.Models.Klarna;

namespace NewsApp.Services
{
    public interface IEmailService
    {
        Task<string> SendEmail(SubscriptionEmail newEmail);
        
    }
}
