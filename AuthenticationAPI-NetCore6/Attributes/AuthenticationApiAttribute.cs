using AuthenticationAPI_NetCore6.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationAPI_NetCore6.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class AuthenticationApiAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Token Client
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthenticationApi_Helpers.TokenCLient, out var collectedTokenClient))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = $"{AuthenticationApi_Helpers.TokenCLient} is required!"
                };
                return;
            }

            // Token Application
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthenticationApi_Helpers.TokenApplication, out var collectedTokenApplication))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = $"{AuthenticationApi_Helpers.TokenApplication} is required!"
                };
                return;
            }

            // Get Tokens
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var tokens = appSettings.GetSection("AuthenticationAPI");
            var _tokenClient = tokens.GetValue<string>(AuthenticationApi_Helpers.TokenCLient);
            var _tokenApplication = tokens.GetValue<string>(AuthenticationApi_Helpers.TokenApplication);

            // Token-Client Validation
            if (!_tokenClient.Equals(collectedTokenClient))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = $"{AuthenticationApi_Helpers.TokenCLient} - Unauthorized!"
                };
                return;
            }

            // Token-Application Validation
            if (!_tokenApplication.Equals(collectedTokenApplication))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = $"{AuthenticationApi_Helpers.TokenApplication} - Unauthorized!"
                };
                return;
            }

            await next();
        }
    }
}
