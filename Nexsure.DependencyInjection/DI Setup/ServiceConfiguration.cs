using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexsure.DataBridge.Repositories.IRepository;
using Nexsure.DataBridge.Repositories.Repository;
namespace Nexsure.DependencyInjection.DI_Setup
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddNexsureServices(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddSingleton<ILogService, LogService>();

            return services;
        }
    }
}
