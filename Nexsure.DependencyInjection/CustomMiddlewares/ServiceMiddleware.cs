using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace Nexsure.DependencyInjection.CustomMiddlewares
{
    public class ServiceMiddleware
    {
        private readonly RequestDelegate _next;

        public ServiceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // You can add pre-processing logic here (e.g., logging)
            Console.WriteLine("ServiceMiddleware executing before next middleware.");

            await _next(context); // Call the next middleware

            // Post-processing logic
            Console.WriteLine("ServiceMiddleware executed after next middleware.");
        }
    }

    public static class ServiceMiddlewareExtensions
    {
        public static IApplicationBuilder UseServiceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ServiceMiddleware>();
        }
    }
}
