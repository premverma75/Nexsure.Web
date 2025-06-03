using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Nexsure.DependencyInjection.CustomMiddlewares
{
    public class DapperConnectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _connectionString;

        public DapperConnectionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _connectionString = configuration.GetConnectionString("NexsureConnection");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            context.Items["DapperConnection"] = connection;
            try
            {
                await _next(context);
            }
            finally
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }
    }

    public static class DapperConnectionMiddlewareExtensions
    {
        public static IApplicationBuilder UseDapperSqlConnectionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SqlConnectionMiddleware>();
        }
    }
}