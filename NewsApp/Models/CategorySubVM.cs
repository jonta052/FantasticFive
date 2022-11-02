namespace NewsApp.Models
{
    public class CategorySubVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CategoriesName { get; set; }
        public string SubTypeName { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }

        
    }
}
