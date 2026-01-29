using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PostMedia.Infrastructure.Orleans
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddOrleans(this IServiceCollection services) =>
            services
                .AddOrleansClient(c => c.UseLocalhostClustering(30001));
    }
}
