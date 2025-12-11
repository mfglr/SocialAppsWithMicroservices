using CommentService.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace CommentService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) =>
            services
                .AddSingleton<MongoContext>()
                .AddSingleton<ICommentRepository, CommentRepository>();
    }
}
