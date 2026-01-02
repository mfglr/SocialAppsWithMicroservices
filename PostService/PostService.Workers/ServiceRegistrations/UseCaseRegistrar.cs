using MassTransit;
using PostService.Application.UseCases.SetPostContentModerationResult;
using PostService.Application.UseCases.SetPostMedia;

namespace PostService.Workers.ServiceRegistrations
{
    internal static class UseCaseRegistrar
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services) =>
            services
                .AddMediator(cfg =>
                {
                    cfg.AddConsumer<SetPostContentModerationResultConsumer>();
                    cfg.AddConsumer<SetPostMediaConsumer>();
                });
    }
}
