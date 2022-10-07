using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Comment")]
        public string Body { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime DateStamp { get; protected set; } = DateTime.Now;

    }
}


