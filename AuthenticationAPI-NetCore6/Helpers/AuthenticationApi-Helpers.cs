namespace AuthenticationAPI_NetCore6.Helpers
{
    /// <summary>
    /// Classe auxiliar para gerenciar tokens de autenticação da API.
    /// Define as constantes dos nomes dos tokens e métodos para extraí-los dos headers HTTP.
    /// </summary>
    public static class AuthenticationApi_Helpers
    {
        /// <summary>
        /// Nome do header HTTP que contém o token do cliente.
        /// </summary>
        public const string TokenClient = "Token-Client";
        
        /// <summary>
        /// Nome do header HTTP que contém o token da aplicação.
        /// </summary>
        public const string TokenApplication = "Token-Application";

        /// <summary>
        /// Obtém o valor do Token-Client do contexto HTTP.
        /// </summary>
        /// <param name="context">O contexto HTTP da requisição.</param>
        /// <returns>O valor do Token-Client ou null se não encontrado.</returns>
        public static string GetTokenClient(HttpContext context)
        {
            context.Request.Headers.TryGetValue(TokenClient, out var collectedTokenClient);
            return collectedTokenClient;
        }

        /// <summary>
        /// Obtém o valor do Token-Application do contexto HTTP.
        /// </summary>
        /// <param name="context">O contexto HTTP da requisição.</param>
        /// <returns>O valor do Token-Application ou null se não encontrado.</returns>
        public static string GetTokenApplication(HttpContext context)
        {
            context.Request.Headers.TryGetValue(TokenApplication, out var collectedTokenApplication);
            return collectedTokenApplication;
        }
    }
}
