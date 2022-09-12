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

        public virtual ICollection<Comment> Comments { get; set; }

    }
    
}
