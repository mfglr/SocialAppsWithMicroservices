using MassTransit;
using ThumbnailGenerator.Workers;
using ThumbnailGenerator.Workers.Consumers.PostDomain;
using ThumbnailGenerator.Workers.Consumers.UserDomain;

namespace ThumbnailGenerator.Workers
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMassTransit(
                x =>
                {
                    //post domain
                    x.AddConsumer<Generate360SquareThumbnail_OnPostMediaClassifed_ThumbnailGenerator>();
                    x.AddConsumer<Generate720Thumbnail_OnPostMediaClassifed_ThumbnailGenerator>();
                    x.AddConsumer<Generate1080Thumbnail_OnPostMediaClassifed_ThumbnailGenerator>();
                    //post domain

                    //user domain
                    x.AddConsumer<Generate64SquareThumbnail_OnUserMediaCreated_ThumbnailGenerator>();
                    x.AddConsumer<Generate128SquareThumbnail_OnUserMediaCreated_ThumbnailGenerator>();
                    x.AddConsumer<Generate256SquareThumbnail_OnUserMediaCreated_ThumbnailGenerator>();
                    x.AddConsumer<Generate720Thumbnail_OnUserMediaCreated_ThumbnailGenerator>();
                    //user domain

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(configuration["RabbitMQ:Host"], configuration["RabbitMQ:VirtualHost"], h =>
                        {
                            h.Username(configuration["RabbitMQ:UserName"]!);
                            h.Password(configuration["RabbitMQ:Password"]!);
                        });
                        cfg.ConfigureEndpoints(context);
                    });
                }
            );
    }
}
