using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.DependencyInjection.CustomMiddlewares
{
    // Middleware/SqlConnectionMiddleware.cs
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Data.SqlClient;
    using System.Data;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;

    public class SqlConnectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _connectionString;

        public SqlConnectionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _connectionString = configuration.GetConnectionString("NexsureConnection");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await using var connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                context.Items["SqlConnection"] = connection;
                await _next(context); // Continue down the pipeline
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }

        
    }
    public static class SqlConnectionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSqlConnectionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SqlConnectionMiddleware>();
        }
    }
}
