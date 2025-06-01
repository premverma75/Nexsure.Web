using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Nexsure.DataBridge.DataContext;

namespace Nexsure.DependencyInjection.CustomMiddlewares
{
    public class SqlConnectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _connectionString;

        public SqlConnectionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            // Use SqlHelper to validate and retrieve the connection string
            _connectionString = configuration.GetConnectionString("NexsureConnection")
                ?? throw new InvalidOperationException("Connection string 'NexsureConnection' not found.");
        }

        public async Task InvokeAsync(HttpContext context, NexsureAppDbContext dbContext)
        {
            // Defensive: Check if connection string is valid
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new InvalidOperationException("SQL connection string is null or empty.");
            }

            SqlConnection? connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);

                // Validate DataSource and InitialCatalog
                if (string.IsNullOrWhiteSpace(connection.DataSource) || string.IsNullOrWhiteSpace(connection.Database))
                {
                    throw new InvalidOperationException("SQL connection string is missing DataSource or InitialCatalog.");
                }

                //await connection.OpenAsync();
                //context.Items["SqlConnection"] = connection;

                //await _next(context); // Continue to the next middleware
               // public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
        
            // Optional: expose DbContext via context.Items for legacy code access
            context.Items["NexsureAppDbContext"] = dbContext;

            // Continue to the next middleware
            await _next(context);
        
    }
            catch (SqlException ex)
            {
                // Log detailed SQL error
                Console.WriteLine($"SQL Connection Middleware Error: {ex.Message} | ErrorCode: {ex.ErrorCode} | Number: {ex.Number}");
                throw;
            }
            catch (Exception ex)
            {
                // Log general error
                Console.WriteLine($"SQL Connection Middleware Error: {ex.Message}");
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
                connection?.Dispose();
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
