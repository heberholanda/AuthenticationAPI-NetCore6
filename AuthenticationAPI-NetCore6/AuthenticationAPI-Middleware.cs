namespace Middleware_NetCore6
{
    public class AuthenticationAPI
    {
        private readonly RequestDelegate _next;

        private const string TokenCLient = "Token-Client";
        private const string TokenApplication = "Token-Application";

        public AuthenticationAPI(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Token Client
            if (!context.Request.Headers.TryGetValue(TokenCLient, out var collectedTokenClient))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{TokenCLient} is required!");
                return;
            }

            // Token Application
            if (!context.Request.Headers.TryGetValue(TokenApplication, out var collectedTokenApplication))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{TokenApplication} is required!");
                return;
            }
            
            // Get Tokens
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var tokens = appSettings.GetSection("AuthenticationAPI");
            var _tokenClient = tokens.GetValue<string>(TokenCLient);
            var _tokenApplication = tokens.GetValue<string>(TokenApplication);

            // Token-Client Validation
            if (!_tokenClient.Equals(collectedTokenClient))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{TokenCLient} - Unauthorized!");
                return;
            }

            // Token-Application Validation
            if (!_tokenApplication.Equals(collectedTokenApplication))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{TokenApplication} - Unauthorized!");
                return;
            }

            await _next(context);
        }
    }
}
