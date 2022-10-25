using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }

    }
    
}
