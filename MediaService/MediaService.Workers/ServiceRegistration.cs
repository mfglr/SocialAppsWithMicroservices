using MassTransit;
using MediaService.Infrastructure;
using MediaService.Workers.Consumers.CreataPostMedia;
using MediaService.Workers.Consumers.CreateMedia;
using MediaService.Workers.Consumers.DeleteMedia;
using MediaService.Workers.Consumers.SetMediaMetadata;
using MediaService.Workers.Consumers.SetMediaModerationResul;
using MediaService.Workers.Consumers.SetMediaThumbnail;
using MediaService.Workers.Consumers.SetMediaTranscodedBlobName;
using System.Reflection;

namespace MediaService.Workers
{
    internal static class ServiceRegistration
    {

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddAutoMapper(
                    cfg => {
                        cfg.LicenseKey = configuration["LuckPenny:LicenseKey"]!;
                    },
                    Assembly.GetExecutingAssembly()
                );


        public static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddMassTransit(
                    x =>
                    {
                        x.AddConsumer<CreateMediaConsumer_MediaService>();
                        x.AddConsumer<DeleteMediaConsumer_MediaService>();
                        x.AddConsumer<SetMediaModerationResult>();
                        x.AddConsumer<SetMediaThumbnailConsumer_MediaService>();
                        x.AddConsumer<SetMediaTranscodedBlobNameConsumer_MediaService>();
                        x.AddConsumer<SetMediaMetadataConsumer_MediaService>();
                        x.AddConsumer<CreatePostMediaConsumer_MediaService>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(configuration["RabbitMQ:Host"], configuration["RabbitMQ:VirtualHost"], h =>
                            {
                                h.Username(configuration["RabbitMQ:UserName"]!);
                                h.Password(configuration["RabbitMQ:Password"]!);
                            });

                            var retryLimit = 4;

                            cfg.ReceiveEndpoint("SetMediaModerationResult", e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryLimit);
                                    cfg.Handle<AppConcurrencyException>();
                                });
                                e.ConfigureConsumer<SetMediaModerationResult>(context);
                            });

                            //720 and 360Squre
                            cfg.ReceiveEndpoint("SetMediaThumbnail", e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryLimit);
                                    cfg.Handle<AppConcurrencyException>();
                                });
                                e.ConfigureConsumer<SetMediaThumbnailConsumer_MediaService>(context);
                            });

                            cfg.ReceiveEndpoint("SetMediaTranscodedBlobName", e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryLimit);
                                    cfg.Handle<AppConcurrencyException>();
                                });
                                e.ConfigureConsumer<SetMediaTranscodedBlobNameConsumer_MediaService>(context);
                            });

                            cfg.ReceiveEndpoint("SetMediaMetadata", e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryLimit);
                                    cfg.Handle<AppConcurrencyException>();
                                });
                                e.ConfigureConsumer<SetMediaMetadataConsumer_MediaService>(context);
                            });

                            cfg.ConfigureEndpoints(context);
                        });
                    }
                );
    }
}
