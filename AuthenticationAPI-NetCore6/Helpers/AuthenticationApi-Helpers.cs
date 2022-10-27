namespace AuthenticationAPI_NetCore6.Helpers
{
    public static class AuthenticationApi_Helpers
    {
        public const string TokenClient = "Token-Client";
        public const string TokenApplication = "Token-Application";

        public static string GetTokenClient(HttpContext context)
        {
            context.Request.Headers.TryGetValue(TokenClient, out var collectedTokenClient);
            return collectedTokenClient;
        }

        public static string GetTokenApplication(HttpContext context)
        {
            context.Request.Headers.TryGetValue(TokenApplication, out var collectedTokenApplication);
            return collectedTokenApplication;
        }
    }
}
