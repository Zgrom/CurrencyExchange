using Microsoft.AspNetCore.Builder;

namespace WebApi.Extensions
{
    public static class StartupAndScheduleMiddlewareExtension
    {
        public static void ConfigureCustomStartupAndScheduleMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<StartupAndScheduleMiddleware.StartupAndScheduleMiddleware>();
        }
    }
}