using Microsoft.EntityFrameworkCore;
using Nexsure.Entities.Domain_Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace Nexsure.DataBridge.DataContext
{
   

    public class NexsureAppDbContext : DbContext
    {
        private readonly SqlConnection _connection;

        public NexsureAppDbContext(DbContextOptions<NexsureAppDbContext> options, SqlConnection connection)
            : base(options)
        {
            _connection = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && _connection != null)
            {
                optionsBuilder.UseSqlServer(_connection);
            }
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }

}
