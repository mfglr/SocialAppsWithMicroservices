using MassTransit;
using PostService.Infrastructure.MongoDb;
using PostService.Workers.Consumers;
using PostService.Workers.ServiceRegistrations;

namespace PostService.Workers.ServiceRegistrations
{
    internal class MassTransitOptions
    {
        public required string Host { get; set; }
        public required string VirtualHost { get; set; }
        public required string Password { get; set; }
        public required string UserName { get; set; }
    }

    internal static class MassTransitRegistrar
    {

        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var option = configuration.GetSection(nameof(MassTransitOptions)).Get<MassTransitOptions>()!;
            return services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<SetPostContentModerationResult_OnPostContentClassified_PostService>();
                    x.AddConsumer<SetPostMedia_OnPostMediaPreproccessingCompleted_PostService>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(option.Host, option.VirtualHost, h =>
                        {
                            h.Username(option.UserName);
                            h.Password(option.Password);
                        });

                        var retryLimit = 1;

                        cfg.ReceiveEndpoint(nameof(SetPostContentModerationResult_OnPostContentClassified_PostService), e =>
                        {
                            e.UseMessageRetry(rc =>
                            {
                                rc.Immediate(retryLimit);
                                rc.Handle<AppConcurrencyException>();
                            });
                            e.ConfigureConsumer<SetPostContentModerationResult_OnPostContentClassified_PostService>(context);
                        });

                        cfg.ReceiveEndpoint(nameof(SetPostMedia_OnPostMediaPreproccessingCompleted_PostService), e =>
                        {
                            e.UseMessageRetry(rc =>
                            {
                                rc.Immediate(retryLimit);
                                rc.Handle<AppConcurrencyException>();
                            });
                            e.ConfigureConsumer<SetPostMedia_OnPostMediaPreproccessingCompleted_PostService>(context);
                        });
                    });

                }
            );
        }
    }
}
