namespace UserManagementAPI.Middleware
{
    public static class AuthenticationExtensions
    {
        public static IApplicationBuilder UseSimpleAuthentication(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}