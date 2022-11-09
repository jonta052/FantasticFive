using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimerTrigger1.Models;

namespace TimerTrigger1.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder
            optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["DefaultConnection"]);
        }
        public DbSet<Article> Articles { get; set; }
    }
}
