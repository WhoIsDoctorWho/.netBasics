using Microsoft.EntityFrameworkCore;


namespace BeerStore.Models
{
    public class BeerContext : DbContext        
    {
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BeerContext(DbContextOptions<BeerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
