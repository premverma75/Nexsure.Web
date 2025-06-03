using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Nexsure.DependencyInjection.DI_Setup;
using Nexsure.Service.FluentValidations;

var builder = WebApplication.CreateBuilder(args);

// Register Nexsure services
builder.Services.AddNexsureServices();

// Register controllers and FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
    });

// Swagger/OpenAPI setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nexsure API", Version = "v1" });
});

var app = builder.Build();

// 👇 Clean, single call to your middleware pipeline
app.UseNexsureApiPipeline();

app.Run();