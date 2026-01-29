using Microsoft.Extensions.DependencyInjection;
using PostMedia.Infrastructure.Orleans;

namespace PostMedia.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
            services
                .AddOrleans();
    }
}
