using Microsoft.EntityFrameworkCore;
namespace Zapchastulkin.Models
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }        
    }
}
