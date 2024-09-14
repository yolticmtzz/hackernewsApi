using Microsoft.Extensions.Configuration;

namespace HackerNewsApi.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        
        private readonly IConfiguration _configuration;
        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            
        }

        public async Task Invoke(HttpContext context)
        {
            var apiKey = _configuration.GetValue<string>("ApiKey");
            if (context.Request.Headers.TryGetValue("apikey", out var extractedApiKey))
            {
                if (!string.Equals(apiKey, extractedApiKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid API Key");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key is missing");
                return;
            }

            await _next(context);
        }
    }

}
