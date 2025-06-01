using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexsure.DataBridge.DataContext;
using Nexsure.DataBridge.Repositories.IRepository;
using Nexsure.DataBridge.Repositories.Repository;
using Nexsure.DependencyInjection.DapperImplementationservice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Nexsure.DependencyInjection.DI_Setup
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddNexsureServices(this IServiceCollection services) // Add IConfiguration parameter
        {
            // Register your services here
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddSingleton<ILogService, LogService>();
            services.AddDbContext<NexsureAppDbContext>(options =>
                options.UseSqlServer(GetConnectionString("NexsureConnection"))); // Corrected to use the 'configuration' parameter directly

            services.AddScoped<IDapperService>(provider =>
            {
                var httpContext = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
                var connection = httpContext?.Items["SqlConnection"] as SqlConnection;

                if (connection == null)
                    throw new InvalidOperationException("SQL connection is not available in context.");

                return new DapperService(connection);
            });

            services.AddHttpContextAccessor();

            return services;
        }

        private static string GetConnectionString(string v)
        {
            // This method should return the connection string from your configuration
            // For example, you can use IConfiguration to get the connection string
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString(v);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"Connection string '{v}' not found.");
            }
            return connectionString;
        }
    }
}
