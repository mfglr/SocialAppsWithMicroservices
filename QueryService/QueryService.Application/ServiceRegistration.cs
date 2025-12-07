using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using QueryService.Application.UseCases.PostUseCases.GetPostById;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;

namespace QueryService.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services.AddMediator(cfg =>
            {
                cfg.AddConsumer<UpdatePostConsumer>();
                cfg.AddConsumer<GetPostByIdConsumer>();
            });
    }
}
