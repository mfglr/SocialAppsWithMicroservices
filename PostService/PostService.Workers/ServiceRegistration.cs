using MassTransit;
using PostService.Infrastructure;
using PostService.Workers;
using PostService.Workers.Consumers.SetPostContentModerationResult;
using PostService.Workers.Consumers.SetPostMedia;
using System.Reflection;

namespace PostService.Workers
{
    internal static class ServiceRegistration
    {

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddAutoMapper(
                    cfg => {
                        cfg.LicenseKey = configuration["AutoMapper:LicenseKey"]!;
                    },
                    Assembly.GetAssembly(typeof(Application.ServiceRegistration)),
                    Assembly.GetExecutingAssembly()
                );

        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configration) =>
            services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<SetPostContentModerationResult_PostService>();
                    x.AddConsumer<SetPostMedia_PostService>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(configration["RabbitMQ:Host"], configration["RabbitMQ:VirtualHost"], h =>
                        {
                            h.Username(configration["RabbitMQ:UserName"]!);
                            h.Password(configration["RabbitMQ:Password"]!);
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
