using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual SubscriptionType SubscriptionType { get; set; }
        public virtual KlarnaOrder KlarnaOrder { get; set; }
        [Required]
        public decimal Price { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
        //Greyed out Subscription if Active = true
        public bool Active { get; set; }
        public virtual User User { get; set; }
        
    }
}
