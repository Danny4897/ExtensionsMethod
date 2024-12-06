using Microsoft.Extensions.DependencyInjection;

namespace ExtensionMethods.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookAuthorService>();
            return services;
        }
    }
}
