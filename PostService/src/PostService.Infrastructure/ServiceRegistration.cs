using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostService.Application;
using PostService.Infrastructure.MongoDb;
using StackExchange.Redis;

namespace PostService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddMongoDb(configuration)
                .AddSingleton(ConnectionMultiplexer.Connect(configuration["Redis:Host"]!))
                .AddSingleton<IAccessTokenProvider, RedisAccessTokenProvider>()
                .AddSingleton<IBlobService, LocalBlobService>();
    }
}
