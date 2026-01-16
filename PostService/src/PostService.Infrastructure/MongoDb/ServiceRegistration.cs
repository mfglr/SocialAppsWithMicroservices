using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostService.Domain;

namespace PostService.Infrastructure.MongoDb
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services,IConfiguration configuration) =>
            services
                .AddScoped<MongoContext>()
                .AddScoped<IPostRepository, PostRepository>();
    }
}
