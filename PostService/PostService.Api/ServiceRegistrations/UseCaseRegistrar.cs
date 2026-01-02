using MassTransit;
using PostService.Application.UseCases.CreatePost;
using PostService.Application.UseCases.CreatePostMedia;
using PostService.Application.UseCases.DeletePost;
using PostService.Application.UseCases.DeletePostMedia;
using PostService.Application.UseCases.RestorePost;
using PostService.Application.UseCases.UpdatePostContent;

namespace PostService.Api.ServiceRegistrations
{
    internal static class UseCaseRegistrar
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services) =>
            services
                .AddMediator(cfg =>
                {
                    cfg.AddConsumer<CreatePostConsumer>();
                    cfg.AddConsumer<DeletePostMediaConsumer>();
                    cfg.AddConsumer<CreatePostMediaConsumer>();
                    cfg.AddConsumer<UpdatePostContentConsumer>();
                    cfg.AddConsumer<DeletePostConsumer>();
                    cfg.AddConsumer<RestorePostConsumer>();
                });
    }
}
