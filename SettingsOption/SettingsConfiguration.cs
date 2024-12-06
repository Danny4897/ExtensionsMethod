using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExtensionMethods.SettingsOption
{
    public static class SettingsConfiguration
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MyOptions>(configuration.GetSection("MyOptions"));
            return services;
        }
    }
}
