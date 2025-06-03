using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Nexsure.DataBridge.DataContext
{
    public class NexsureAppDbContextFactory : IDesignTimeDbContextFactory<NexsureAppDbContext>
    {
        public NexsureAppDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\Nexsure.API");
            // Locate the appsettings.json (adjust if needed)
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("NexsureConnection");

            var optionsBuilder = new DbContextOptionsBuilder<NexsureAppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new NexsureAppDbContext(optionsBuilder.Options);
        }
    }
}