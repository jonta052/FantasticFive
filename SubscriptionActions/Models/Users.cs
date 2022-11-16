using Microsoft.AspNetCore.Identity;

namespace SubscriptionActions.Models
{
    internal class Users : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

    }
}
