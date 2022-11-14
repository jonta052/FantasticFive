namespace SubscriptionActions.Models
{
    internal class Article
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string ImageLink { get; set; }
        public DateTime DateStamp { get; set; }
    }
}
