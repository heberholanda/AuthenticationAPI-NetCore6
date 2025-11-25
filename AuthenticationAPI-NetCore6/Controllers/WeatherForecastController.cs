using AuthenticationAPI_NetCore6.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI_NetCore6.Controllers
{
    /// <summary>
    /// Controller de exemplo que demonstra diferentes cenários de autenticação.
    /// Inclui endpoints protegidos pelo middleware de autenticação e endpoints anônimos.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Retorna uma lista de previsões meteorológicas com os tokens de autenticação utilizados.
        /// Este endpoint está protegido pelo middleware de autenticação global.
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

        /// <summary>
        /// Retorna uma lista de previsões meteorológicas sem autenticação.
        /// O atributo [AllowAnonymous] permite acesso sem validação de tokens,
        /// mesmo com o middleware de autenticação ativo.
        /// </summary>
        /// <returns>Uma coleção de previsões meteorológicas sem informações de token.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<WeatherForecast> GetAllAnonymous()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}