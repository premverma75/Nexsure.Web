using Nexsure.DependencyInjection.DI_Setup;
using Nexsure.DependencyInjection.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddNexsureServices();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ✨ Single clean call
app.UseNexsureWebPipeline();

app.Run();
