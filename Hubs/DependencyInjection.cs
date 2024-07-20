using Microsoft.Extensions.DependencyInjection;

namespace Hubs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHubs(this IServiceCollection services)
        {
            //services.AddSignalRCore();

            return services;
        }
    }
}
