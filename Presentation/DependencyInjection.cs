using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            var assembly = Assembly.Load("Presentation");

            services
                .AddControllers()
                .AddApplicationPart(assembly);

            return services;
        }
    }
}
