using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    
    public class Article
    {
        public int Id { get; set; }

        [DisplayName("Some Title")]
        [Required(ErrorMessage = "The title is required!")]
        public string Title { get; set; } /*= String.Empty;*/
        [Required]
        public DateTime DateStamp { get; protected set; } = DateTime.Now;
       
        public string LinkText { get; set; }
        public string ContentSummary { get; set; }
        public string Content { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public string ImageLink { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
    
}
