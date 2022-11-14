namespace SubscriptionActions.Models
{
    internal class Subscription
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public bool Active { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
