using SubscriptionActions.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SubscriptionActions.Data
{
    internal class AppDbContext : IdentityDbContext<Users>
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder.UseSqlServer(_configuration["DefaultConnection"]);
        }
        public DbSet<Subscription> Subscriptions { get;set;}
        public DbSet<Users> User { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<UserCategories> UserCategories { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
