namespace NewsApp.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public virtual ICollection<SubscriptionType> SubscriptionTypes { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
        //Greyed out Subscription if IsActive = true
        public bool IsActive { get; set; }
        public virtual User User { get; set; }
        public bool KlarnaOrder { get; set; }
    }
}
