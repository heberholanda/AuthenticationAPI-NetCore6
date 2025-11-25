using AuthenticationAPI_NetCore6.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationAPI_NetCore6.Attributes
{
    /// <summary>
    /// Atributo de autenticação que valida os tokens Token-Client e Token-Application
    /// antes da execução de uma action do controller. Este atributo pode ser usado como
    /// alternativa ao middleware global, aplicando autenticação apenas aos controllers específicos.
    /// Os tokens esperados são configurados no appsettings.json na seção "AuthenticationAPI".
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class AuthenticationApiAttribute : Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// Método executado antes da action do controller.
        /// Valida a presença e autenticidade dos tokens antes de permitir a execução da action.
        /// </summary>
        /// <param name="context">O contexto de execução da action.</param>
        /// <param name="next">O delegate para executar a próxima action no pipeline.</param>
        /// <returns>Uma Task representando a operação assíncrona.</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Verifica se o Token-Client está presente nos headers da requisição
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthenticationApi_Helpers.TokenClient, out var collectedTokenClient))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = $"{AuthenticationApi_Helpers.TokenClient} is required!"
                };
                return;
            }

            // Verifica se o Token-Application está presente nos headers da requisição
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthenticationApi_Helpers.TokenApplication, out var collectedTokenApplication))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = $"{AuthenticationApi_Helpers.TokenApplication} is required!"
                };
                return;
            }

            // Obtém os tokens esperados da configuração (appsettings.json)
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var tokens = appSettings.GetSection("AuthenticationAPI");
            var _tokenClient = tokens.GetValue<string>(AuthenticationApi_Helpers.TokenClient);
            var _tokenApplication = tokens.GetValue<string>(AuthenticationApi_Helpers.TokenApplication);

            // Valida se o Token-Client fornecido corresponde ao token configurado
            if (!_tokenClient.Equals(collectedTokenClient))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = $"{AuthenticationApi_Helpers.TokenClient} - Unauthorized!"
                };
                return;
            }

            // Valida se o Token-Application fornecido corresponde ao token configurado
            if (!_tokenApplication.Equals(collectedTokenApplication))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = $"{AuthenticationApi_Helpers.TokenApplication} - Unauthorized!"
                };
                return;
            }

            // Se todos os tokens forem válidos, executa a action do controller
            await next();
        }
    }
}
