using Microsoft.EntityFrameworkCore;
using Nexsure.DataBridge.DataContext;
using Nexsure.DependencyInjection.CustomMiddlewares;
using Nexsure.DependencyInjection.DI_Setup;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<NexsureAppDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("NexsureConnection")));
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
