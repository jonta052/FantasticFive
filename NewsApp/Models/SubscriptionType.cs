using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class SubscriptionType
    {
        public int Id { get; set; }

        [Required]
        public string TypeName { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        
    }
}
