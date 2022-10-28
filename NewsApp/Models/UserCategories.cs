using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class UserCategories
    {
        public int Id { get; set; }


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
