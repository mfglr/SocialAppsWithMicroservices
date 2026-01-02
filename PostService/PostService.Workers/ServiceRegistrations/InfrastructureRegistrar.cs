using PostService.Application;
using PostService.Domain;
using PostService.Infrastructure;

namespace PostService.Workers.ServiceRegistrations
{
    internal static class InfrastructureRegistrar
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped<IBlobService, LocalBlobService>()
                .AddScoped<MongoContext>()
                .AddScoped<IPostRepository, PostRepository>();
    }
}
