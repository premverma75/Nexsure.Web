using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Nexsure.DependencyInjection.CustomMiddlewares;
using Nexsure.DependencyInjection.Routes.EndPoints;

namespace Nexsure.DependencyInjection.DI_Setup
{
    public static class MiddlewareSetup
    {
        public static WebApplication UseNexsureApiPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nexsure API v1");
                });
            }
            app.UseDapperSqlConnectionMiddleware();
            app.UseSqlConnectionMiddleware();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseServiceMiddleware(); // Optional: Custom logging/monitoring/etc.

            app.MapUserEndpoints();     // Minimal APIs (e.g., /users, etc.)
            app.MapControllers();       // Regular API controllers
            app.MapGet("/", () => Results.Redirect("/swagger")); // Root redirect

            return app;
        }
    }
}