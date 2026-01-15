using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostService.Application;
using PostService.Domain;

namespace PostService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped<IBlobService, LocalBlobService>()
                .AddScoped<MongoContext>()
                .AddScoped<IPostRepository, PostRepository>();
    }
}
