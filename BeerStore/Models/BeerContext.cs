using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BeerStore.Models
{
    public class BeerStoreContext : DbContext        
    {
        public DbSet<Beer> Beers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShopListItem> BeerIds { get; set; } // realization of one-to-many relation

        public BeerStoreContext(DbContextOptions<BeerStoreContext> options) : base(options)
        {
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
    }
}
