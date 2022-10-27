namespace NewsApp.Models
{
    public class SubscriptionVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual SubscriptionType SubscriptionType { get; set; }
    
        public string TypeName { get; set; }
        public string Description { get; set; }
 
        public decimal Price { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
        //Greyed out Subscription if Active = true
        public bool Active { get; set; }
        public virtual User User { get; set; }
    }
}
