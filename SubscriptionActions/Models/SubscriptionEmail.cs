
using System.Text.Json.Serialization;
public class SubscriptionEmail
{
    [JsonPropertyName("subscriberEmail")]
    public string SubscriberEmail { get; set; }
    [JsonPropertyName("subscriberName")]
    public string SubscriberName { get; set; }
    [JsonPropertyName("subscriptionTypeName")]
    public string SubscriptionTypeName { get; set; }
    
    [JsonPropertyName("articleId")]

    public List<int> ArticleId { get; set; }
    [JsonPropertyName("categoryName")]
    public List<string> CategoryName { get; set; }
    [JsonPropertyName("articleImageLink")]
    public List<string> ArticleImageLink { get; set; }
    [JsonPropertyName("articleTitle")]

    public List<string> ArticleTitle { get; set; }

    public SubscriptionEmail()
    {
        ArticleId = new List<int>();
        CategoryName = new List<string>();
        ArticleImageLink = new List<string>();
        ArticleTitle = new List<string>();
    }
}
