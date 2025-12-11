using ContentModerator.Worker;
using MassTransit;

namespace ContentModerator.Worker
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configration) =>
            services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<ClassifyMedia>();
                    x.AddConsumer<ClassifyPostContentOnPostCreated>();
                    x.AddConsumer<ClassifyPostContentOnPostContentUpdated>();
                    x.AddConsumer<ClassifyCommentContentOnCommentCreated>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(configration["RabbitMQ:Host"], configration["RabbitMQ:VirtualHost"], h =>
                        {
                            h.Username(configration["RabbitMQ:UserName"]!);
                            h.Password(configration["RabbitMQ:Password"]!);
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                }
            );
    }
}
