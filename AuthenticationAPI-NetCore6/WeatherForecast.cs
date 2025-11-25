namespace AuthenticationAPI_NetCore6
{
    /// <summary>
    /// Representa uma previsão meteorológica com informações de temperatura, data e tokens de autenticação.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Data da previsão meteorológica.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperatura em graus Celsius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Temperatura em graus Fahrenheit (calculada automaticamente a partir de TemperatureC).
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Descrição resumida das condições meteorológicas.
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        /// Token do cliente utilizado na requisição (para fins de demonstração).
        /// </summary>
        public string? TokenClient { get; set; }
        
        /// <summary>
        /// Token da aplicação utilizado na requisição (para fins de demonstração).
        /// </summary>
        public string? TokenApplication { get; set; }
    }
}