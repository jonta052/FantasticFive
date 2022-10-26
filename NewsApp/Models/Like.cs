using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class Like
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
        public virtual User User { get; set; }

    }
}
