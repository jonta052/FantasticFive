namespace NewsApp.Models.Email
{
    public class SubscriptionEmail
    {
        
        public string SubscriberEmail { get; set; }
        public string SubscriberName { get; set; }
        public string SubscriptionTypeName { get; set; }

        public List<string> CategoryName { get; set; }

        public List<string> ArticleImageLink { get; set; }
        public List<string> ArticleTitle { get; set; }

        public List<int> ArticleId { get; set; }

    }
}
