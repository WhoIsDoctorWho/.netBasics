using Microsoft.EntityFrameworkCore;
namespace BeerStoreSPA.Models
{     
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Beer> Products { get; set; }
    }    
}
