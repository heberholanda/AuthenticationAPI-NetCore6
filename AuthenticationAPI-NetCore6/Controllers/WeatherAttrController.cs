using AuthenticationAPI_NetCore6.Attributes;
using AuthenticationAPI_NetCore6.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI_NetCore6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AuthenticationApiAttribute]
    public class WeatherAttrController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherAttrController> _logger;

        public WeatherAttrController(ILogger<WeatherAttrController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                TokenClient = AuthenticationApi_Helpers.GetTokenClient(HttpContext),
                TokenApplication = AuthenticationApi_Helpers.GetTokenApplication(HttpContext)
            })
            .ToArray();
        }
    }
}