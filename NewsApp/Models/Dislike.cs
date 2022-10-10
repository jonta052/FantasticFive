namespace NewsApp.Models
{
    public class Dislike
    {
            public int Id { get; set; }
            public string UserId { get; set; }
            public int ArticleId { get; set; }

            public virtual Article Article { get; set; }
            public virtual User User { get; set; }

        
    }
}
