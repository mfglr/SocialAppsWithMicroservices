using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using UserService.Application;
using UserService.Infrastructure.Grains;
using UserService.Infrastructure.Keycloak;

namespace UserService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(ConnectionMultiplexer.Connect(configuration["Redis:Host"]!))
                .AddSingleton<IAccessTokenProvider, RedisAccessTokenProvider>()
                .AddSingleton<IBlobService,LocalBlobService>()
                .AddKeycloak(configuration)
                .AddOrleans();
    }
}
