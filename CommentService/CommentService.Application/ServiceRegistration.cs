using CommentService.Application.UseCases.CreateComment;
using CommentService.Application.UseCases.SetCommentContentModerationResult;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CommentService.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services
                .AddMediator(cfg =>
                {
                    cfg.AddConsumer<CreateCommentConsumer>();
                    cfg.AddConsumer<SetCommentContentModerationResultConsumer>();
                });
    }
}
