using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using ThumbnailGenerator.Application;

namespace ThumbnailGenerator.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(ConnectionMultiplexer.Connect(configuration["Redis:Host"]!))
                .AddSingleton<IAccessTokenProvider, RedisAccessTokenProvider>()
                .AddSingleton<IBlobService,LocalBlobService>()
                .AddScoped<IThumbnailGenerator, ThumbnailGenerator>();

    }
}
