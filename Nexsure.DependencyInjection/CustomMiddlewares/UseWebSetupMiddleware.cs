using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Nexsure.DependencyInjection.CustomMiddlewares
{
    public static class UseWebSetupMiddleware
    {
        public static WebApplication UseNexsureWebPipeline(this WebApplication app)
        {

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseDapperSqlConnectionMiddleware();
            app.UseSqlConnectionMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(); // Make sure this comes after UseRouting and before endpoints

            app.UseAuthorization();

            // Optional: Add custom middleware here if needed
            app.UseServiceMiddleware(); // If applicable

            app.MapRazorPages()
               .WithStaticAssets(); // Your RazorPages extension method

            app.MapStaticAssets(); // Custom static map if you have it

            return app;
        }
    }
}
