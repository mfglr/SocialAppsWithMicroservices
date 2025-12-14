using CommentService.Application.UseCases.CreateComment;
using CommentService.Application.UseCases.DeleteComentReplies;
using CommentService.Application.UseCases.DeleteComment;
using CommentService.Application.UseCases.DeletePostComments;
using CommentService.Application.UseCases.RestoreComment;
using CommentService.Application.UseCases.RestoreCommentReplies;
using CommentService.Application.UseCases.RestorePostComments;
using CommentService.Application.UseCases.SetCommentContentModerationResult;
using CommentService.Application.UseCases.UpdateCommentContent;
using CommentService.Domain;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CommentService.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services
                .AddScoped<CommentCreatorDomainService>()
                .AddMediator(cfg =>
                {
                    cfg.AddConsumer<CreateCommentConsumer>();
                    cfg.AddConsumer<SetCommentContentModerationResultConsumer>();
                    cfg.AddConsumer<UpdateCommentContentConsumer>();
                    cfg.AddConsumer<DeleteCommentConsumer>();
                    cfg.AddConsumer<DeleteCommentRepliesConsumer>();
                    cfg.AddConsumer<RestoreCommentConsumer>();
                    cfg.AddConsumer<RestoreCommentRepliesConsumer>();
                    cfg.AddConsumer<DeletePostCommentsConsumer>();
                    cfg.AddConsumer<RestorePostCommentsConsumer>();
                });
    }
}
