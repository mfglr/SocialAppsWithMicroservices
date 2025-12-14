using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostService.Application;
using PostService.Domain;
using StackExchange.Redis;

namespace PostService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped<IBlobService,LocalBlobService>()
                .AddScoped<MongoContext>()
                .AddScoped<IPostRepository, PostRepository>();
    }
}
