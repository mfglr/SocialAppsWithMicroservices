using MassTransit;
using PostService.Infrastructure;
using PostService.Workers;

namespace PostService.Workers
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configration) =>
            services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<SetContentModerationResult>();
                    x.AddConsumer<SetMedia>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(configration["RabbitMQ:Host"], configration["RabbitMQ:VirtualHost"], h =>
                        {
                            h.Username(configration["RabbitMQ:UserName"]!);
                            h.Password(configration["RabbitMQ:Password"]!);
                        });

                        var retryLimit = 5;

                        cfg.ReceiveEndpoint("SetContentModerationResult", e =>
                        {
                            e.UseMessageRetry(rc =>
                            {
                                rc.Immediate(retryLimit);
                                rc.Handle<AppConcurrencyException>();
                            });
                            e.ConfigureConsumer<SetContentModerationResult>(context);
                        });

                        cfg.ReceiveEndpoint("SetMedia", e =>
                        {
                            e.UseMessageRetry(rc =>
                            {
                                rc.Immediate(retryLimit);
                                rc.Handle<AppConcurrencyException>();
                            });
                            e.ConfigureConsumer<SetMedia>(context);
                        });
                    });

                }
            );
    }
}
