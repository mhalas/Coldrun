using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ColdrunContext: DbContext
    {
        public ColdrunContext(DbContextOptions<ColdrunContext> options)
            : base(options) { }

        public DbSet<Truck> Truck { get; set; }

    }
}
