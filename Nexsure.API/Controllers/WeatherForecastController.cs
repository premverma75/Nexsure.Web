using Microsoft.AspNetCore.Mvc;
using Nexsure.DataBridge.DataContext;

namespace Nexsure.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly NexsureAppDbContext _dbContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, NexsureAppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _dbContext.Database.EnsureCreated(); // Ensure the database is created
        // Example: Fetch WeatherForecasts from the database
        // return _dbContext.WeatherForecasts.ToList();

        // Fallback to sample data if DB is empty or for demonstration
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
