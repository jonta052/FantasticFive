using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Services;

namespace NewsApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context == null || context.Articles == null)
                {
                    throw new ArgumentNullException("Null ApplicationDbContext");
                }

                // Look for any categories.
                if (context.Categories.Any())
                {
                    return;   // DB has been seeded
                }
                Category categoryLocal = new Category
                {
                    Name = "Local"
                };
                Category categoryWorld = new Category
                {
                    Name = "World"
                };
                //context.Categories.AddRange(
                //    categoryLocal,
                //    categoryWorld

                //);

                // Look for any articles.
                if (context.Articles.Any())
                {
                    return;   // DB has been seeded
                }

                context.Articles.AddRange(
                    new Article
                    {
                        
                        Title = "Local article",
                        LinkText = "Another title",
                        Content = "",
                        ContentSummary = "Romantic Comedy",
                        ImageLink = "http....",
                        Category = categoryLocal
                        
                    },

                    new Article
                    {
                        
                        Title = "World Article",
                        LinkText = "",
                        Content = "",
                        ContentSummary = "Romantic Comedy",
                        ImageLink = "http....",
                        Category = categoryLocal
                    }
                );
               
                context.SaveChanges();
            }
        }
    }
}
