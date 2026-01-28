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
                    x.AddConsumer<SetPostContentModerationResultConsumer_PostService>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(option.Host, option.VirtualHost, h =>
                        {
                            h.Username(option.UserName);
                            h.Password(option.Password);
                        });

                        var retryLimit = 5;

                        cfg.ReceiveEndpoint(nameof(SetPostContentModerationResultConsumer_PostService), e =>
                        {
                            e.UseMessageRetry(rc =>
                            {
                                rc.Immediate(retryLimit);
                                rc.Handle<AppConcurrencyException>();
                            });
                            e.ConfigureConsumer<SetPostContentModerationResultConsumer_PostService>(context);
                        });
                    });

                }
            );
        }
    }
}
