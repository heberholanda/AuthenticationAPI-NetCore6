using AuthenticationAPI_NetCore6.Attributes;
using AuthenticationAPI_NetCore6.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI_NetCore6.Controllers
{
    /// <summary>
    /// Controller de exemplo que demonstra o uso do atributo [AuthenticationApiAttribute]
    /// para autenticação baseada em tokens. Este controller retorna previsões meteorológicas
    /// e exibe os tokens utilizados na requisição.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [AuthenticationApiAttribute] // Aplica autenticação via atributo apenas neste controller
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

        /// <summary>
        /// Retorna uma lista de previsões meteorológicas com os tokens de autenticação utilizados.
        /// Este endpoint requer autenticação via Token-Client e Token-Application.
        /// </summary>
        /// <returns>Uma coleção de previsões meteorológicas incluindo os tokens recebidos.</returns>
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