using MassTransit;
using VideoTranscoder.Worker;
using VideoTranscoder.Worker.Consumers;

namespace VideoTranscoder.Worker
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<TranscodePostVideo_OnPostMediaClassified_VideoTranscoder>();
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
