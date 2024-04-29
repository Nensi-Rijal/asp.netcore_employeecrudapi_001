using eployeecrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace eployeecrudApi.Data
{
    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions options) : base(options)
        {
        }

        //DbSet = property and is used by entity framework core and acts as a table in SqlServer
        public DbSet<Card> Cards { get; set; }

    }
}
