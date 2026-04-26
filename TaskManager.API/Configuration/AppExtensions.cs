namespace TaskManager.API.Configuration
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseApplicationMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            app.MapGet("/", () => Results.Redirect("/swagger"));

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
