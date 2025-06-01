using Microsoft.EntityFrameworkCore;
using Nexsure.Entities.Domain_Models.Model;

namespace Nexsure.DataBridge.DataContext
{
    public class NexsureAppDbContext : DbContext
    {
        public NexsureAppDbContext(DbContextOptions<NexsureAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
