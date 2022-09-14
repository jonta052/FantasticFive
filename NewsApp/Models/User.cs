using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace NewsApp.Models
{
    public class User: IdentityUser
    {
        [PersonalData]
        [StringLength(20,MinimumLength =2)]
        public string FirstName { get; set; }

        [PersonalData]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        [PersonalData]
        public string DOB { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
      
        public DateTime CreatedAt { get; protected set; } = DateTime.Now;

        public virtual ICollection<Comment> Comments { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }


    }
}
