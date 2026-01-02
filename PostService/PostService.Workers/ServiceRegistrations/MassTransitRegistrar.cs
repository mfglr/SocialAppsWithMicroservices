using MassTransit;
using PostService.Infrastructure;
using PostService.Workers.Consumers.SetPostContentModerationResult;
using PostService.Workers.Consumers.SetPostMedia;
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
                    x.AddConsumer<SetPostContentModerationResult_PostService>();
                    x.AddConsumer<SetPostMedia_PostService>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(option.Host, option.VirtualHost, h =>
                        {
                            h.Username(option.UserName);
                            h.Password(option.Password);
                        });

                        var retryLimit = 5;

                        cfg.ReceiveEndpoint("SetPostContentModerationResult_PostService", e =>
                        {
                            e.UseMessageRetry(rc =>
                            {
                                rc.Immediate(retryLimit);
                                rc.Handle<AppConcurrencyException>();
                            });
                            e.ConfigureConsumer<SetPostContentModerationResult_PostService>(context);
                        });

                        cfg.ReceiveEndpoint("SetPostMedia_PostService", e =>
                        {
                            e.UseMessageRetry(rc =>
                            {
                                rc.Immediate(retryLimit);
                                rc.Handle<AppConcurrencyException>();
                            });
                            e.ConfigureConsumer<SetPostMedia_PostService>(context);
                        });
                    });

                }
            );
        }
    }
}
