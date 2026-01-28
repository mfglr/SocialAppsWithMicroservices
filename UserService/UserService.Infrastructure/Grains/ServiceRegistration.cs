using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UserService.Infrastructure.Grains
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddOrleans(this IServiceCollection services) =>
            services
                .AddOrleansClient(c => c.UseLocalhostClustering());
    }
}
