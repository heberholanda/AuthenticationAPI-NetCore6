using AuthenticationAPI_NetCore6.Helpers;

namespace AuthenticationAPI_NetCore6
{
    /// <summary>
    /// Middleware de autenticação que valida os tokens Token-Client e Token-Application
    /// em todas as requisições HTTP. Os tokens esperados são configurados no appsettings.json
    /// na seção "AuthenticationAPI".
    /// </summary>
    public class AuthenticationApi
    {
        private readonly RequestDelegate _next;

        public AuthenticationApi(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Método invocado para cada requisição HTTP.
        /// Valida a presença e autenticidade dos tokens antes de permitir o processamento da requisição.
        /// </summary>
        /// <param name="context">O contexto HTTP da requisição.</param>
        /// <returns>Uma Task representando a operação assíncrona.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // Verifica se o Token-Client está presente nos headers da requisição
            if (!context.Request.Headers.TryGetValue(AuthenticationApi_Helpers.TokenClient, out var collectedTokenClient))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{AuthenticationApi_Helpers.TokenClient} is required!");
                return;
            }

            // Verifica se o Token-Application está presente nos headers da requisição
            if (!context.Request.Headers.TryGetValue(AuthenticationApi_Helpers.TokenApplication, out var collectedTokenApplication))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{AuthenticationApi_Helpers.TokenApplication} is required!");
                return;
            }
            
            // Obtém os tokens esperados da configuração (appsettings.json)
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var tokens = appSettings.GetSection("AuthenticationAPI");
            var _tokenClient = tokens.GetValue<string>(AuthenticationApi_Helpers.TokenClient);
            var _tokenApplication = tokens.GetValue<string>(AuthenticationApi_Helpers.TokenApplication);

            // Valida se o Token-Client fornecido corresponde ao token configurado
            if (!_tokenClient.Equals(collectedTokenClient))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{AuthenticationApi_Helpers.TokenClient} - Unauthorized!");
                return;
            }

            // Valida se o Token-Application fornecido corresponde ao token configurado
            if (!_tokenApplication.Equals(collectedTokenApplication))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{AuthenticationApi_Helpers.TokenApplication} - Unauthorized!");
                return;
            }

            // Se todos os tokens forem válidos, prossegue para o próximo middleware na pipeline
            await _next(context);
        }
    }
}
