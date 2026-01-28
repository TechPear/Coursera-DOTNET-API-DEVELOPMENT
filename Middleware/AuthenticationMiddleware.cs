using System.Text.Json;

namespace UserManagementAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ValidToken = "my-secret-token"; // Example token

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip authentication for Swagger and root endpoints
            var path = context.Request.Path.Value?.ToLower();
            if (path != null && (path.Contains("swagger") || path == "/"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                await WriteUnauthorizedResponse(context, "Missing Authorization header");
                return;
            }

            if (!token.ToString().Equals(ValidToken, StringComparison.Ordinal))
            {
                await WriteUnauthorizedResponse(context, "Invalid or expired token");
                return;
            }

            await _next(context);
        }

        private async Task WriteUnauthorizedResponse(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                error = "Unauthorized",
                detail = message
            };

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}