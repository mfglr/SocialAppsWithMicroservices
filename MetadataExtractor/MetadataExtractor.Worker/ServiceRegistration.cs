using MassTransit;
using MetadataExtractor.Worker;
using MetadataExtractor.Worker.Consumers.PostDomain;
using MetadataExtractor.Worker.Consumers.UserDomain;

namespace MetadataExtractor.Worker
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configration) =>
            services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<ExtractMetadata_OnUserMediaCreated_MetadataExtractor>();
                    x.AddConsumer<ExtractMetadata_OnPostMediaCreated_MetadataExtractor>();
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
