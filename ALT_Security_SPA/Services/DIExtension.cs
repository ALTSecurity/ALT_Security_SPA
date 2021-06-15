using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ALT_Security_SPA.Services
{
    public static class DIExtension
    {
        public static void AddApp(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtConfig>(options => config.GetSection("AuthToken").Bind(options));

            services.AddSingleton<IJwtService, JwtService>();
        }
    }
}
