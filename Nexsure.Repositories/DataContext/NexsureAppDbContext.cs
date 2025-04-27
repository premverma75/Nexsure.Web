using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.DataBridge.DataContext
{
    public class NexsureAppDbContext : DbContext
    {
        public NexsureAppDbContext(DbContextOptions<NexsureAppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("NexsureConnection");
            }
        }

        // Add DbSet properties for your entities here
        // Example:
        // public DbSet<YourEntity> YourEntities { get; set; }
    }
}
