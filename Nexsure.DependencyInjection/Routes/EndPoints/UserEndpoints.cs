// UserEndpoints.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Nexsure.DependencyInjection.Routes.EndPoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/users/hello", () => "Hello from Minimal API!");

            app.MapPost("/api/users/create", (UserDto user) =>
            {
                // Do something with the user (mocked)
                return Results.Ok($"User {user.Name} created.");
            });

            return app;
        }
    }

    public record UserDto(string Name, int Age);
}