using MassTransit;
using ThumbnailGenerator.Workers;
using ThumbnailGenerator.Workers.Consumers.UserDomain;

namespace ThumbnailGenerator.Workers
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMassTransit(
                x =>
                {
                    //x.AddConsumer<Generate720Thumbnail_OnMediaMetadataExtractedSuccess_ThumbnailGenerator>();
                    //x.AddConsumer<Generate360SquareThumbnail_OnMediaMetadaExtractedSuccess_ThumbnailGenerator>();
                    x.AddConsumer<Generate64SquareThumbnail_OnUserMediaCreated_ThumbnailGenerator>();
                    x.AddConsumer<Generate128SquareThumbnail_OnUserMediaCreated_ThumbnailGenerator>();
                    x.AddConsumer<Generate256SquareThumbnail_OnUserMediaCreated_ThumbnailGenerator>();
                    x.AddConsumer<Generate720Thumbnail_OnUserMediaCreated_ThumbnailGenerator>();

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
