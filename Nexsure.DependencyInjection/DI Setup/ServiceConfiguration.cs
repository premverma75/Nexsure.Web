using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexsure.DataBridge.Repositories.IRepository;
using Nexsure.DataBridge.Repositories.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Nexsure.DependencyInjection.DapperImplementationservice;
namespace Nexsure.DependencyInjection.DI_Setup
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddNexsureServices(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddSingleton<ILogService, LogService>();
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
    }
}
