using Microsoft.OpenApi.Models; // Add this using directive at the top of the file
using Swashbuckle.AspNetCore.Swagger; // Ensure this using directive is added
using Swashbuckle.AspNetCore.SwaggerUI; // Ensure this using directive is added

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nexsure API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nexsure API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Ensure a default route is configured to handle requests to the root URL
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
