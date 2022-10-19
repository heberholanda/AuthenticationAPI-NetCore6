using AuthenticationAPI_NetCore6.Helpers;

namespace AuthenticationAPI_NetCore6
{
    public class AuthenticationApi
    {
        private readonly RequestDelegate _next;

        public AuthenticationApi(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Token Client
            if (!context.Request.Headers.TryGetValue(AuthenticationApi_Helpers.TokenCLient, out var collectedTokenClient))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{AuthenticationApi_Helpers.TokenCLient} is required!");
                return;
            }

            // Token Application
            if (!context.Request.Headers.TryGetValue(AuthenticationApi_Helpers.TokenApplication, out var collectedTokenApplication))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{AuthenticationApi_Helpers.TokenApplication} is required!");
                return;
            }
            
            // Get Tokens
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var tokens = appSettings.GetSection("AuthenticationAPI");
            var _tokenClient = tokens.GetValue<string>(AuthenticationApi_Helpers.TokenCLient);
            var _tokenApplication = tokens.GetValue<string>(AuthenticationApi_Helpers.TokenApplication);

            // Token-Client Validation
            if (!_tokenClient.Equals(collectedTokenClient))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{AuthenticationApi_Helpers.TokenCLient} - Unauthorized!");
                return;
            }

            // Token-Application Validation
            if (!_tokenApplication.Equals(collectedTokenApplication))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{AuthenticationApi_Helpers.TokenApplication} - Unauthorized!");
                return;
            }

            await _next(context);
        }
    }
}
