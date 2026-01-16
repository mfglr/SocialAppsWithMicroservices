using Microsoft.Extensions.DependencyInjection;
using QueryService.Application.QueryRepositories;

namespace QueryService.Infrastructure.QueryRepositories
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddQueryRepositories(this IServiceCollection services) =>
            services
                .AddScoped<ICommentQueryRepository,CommentQueryRepository>()
                .AddScoped<IPostQueryRepository, PostQueryRepository>();
    }
}
