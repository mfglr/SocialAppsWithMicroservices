using MassTransit;
using MediaService.Infrastructure;
using MediaService.Workers.Consumers.CreataPostMedia;
using MediaService.Workers.Consumers.CreateMedia;
using MediaService.Workers.Consumers.DeleteMedia;
using MediaService.Workers.Consumers.SetMediaMetadata;
using MediaService.Workers.Consumers.SetMediaModerationResult;
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
                        x.AddConsumer<CreateMedia_OnPostCreated_MediaService>();
                        x.AddConsumer<DeleteMedia_OnMediaPreprocessingCompleted_MediaService>();
                        x.AddConsumer<SetMediaModerationResult_OnMediaClassfied_MediaService>();
                        x.AddConsumer<SetMediaThumbnail_OnMediaThumbnailGenerated_MediaService>();
                        x.AddConsumer<SetMediaTranscodedBlobName_OnVideoTranscoded_MediaService>();
                        x.AddConsumer<SetMediaMetadata_OnMediaMetadataExtractionSuccess_MediaService>();
                        x.AddConsumer<SetMediaMetadata_OnMediaMetadataExtractionFailed_MediaService>();
                        x.AddConsumer<CreatePostMedia_OnPostMediaCreated_MediaService>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(configuration["RabbitMQ:Host"], configuration["RabbitMQ:VirtualHost"], h =>
                            {
                                h.Username(configuration["RabbitMQ:UserName"]!);
                                h.Password(configuration["RabbitMQ:Password"]!);
                            });

                            var retryLimit = 4;

                            cfg.ReceiveEndpoint(nameof(SetMediaModerationResult_OnMediaClassfied_MediaService), e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryLimit);
                                    cfg.Handle<AppConcurrencyException>();
                                });
                                e.ConfigureConsumer<SetMediaModerationResult_OnMediaClassfied_MediaService>(context);
                            });

                            //720 and 360Squre
                            cfg.ReceiveEndpoint(nameof(SetMediaThumbnail_OnMediaThumbnailGenerated_MediaService), e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryLimit);
                                    cfg.Handle<AppConcurrencyException>();
                                });
                                e.ConfigureConsumer<SetMediaThumbnail_OnMediaThumbnailGenerated_MediaService>(context);
                            });

                            cfg.ReceiveEndpoint(nameof(SetMediaTranscodedBlobName_OnVideoTranscoded_MediaService), e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryLimit);
                                    cfg.Handle<AppConcurrencyException>();
                                });
                                e.ConfigureConsumer<SetMediaTranscodedBlobName_OnVideoTranscoded_MediaService>(context);
                            });

                            cfg.ReceiveEndpoint(nameof(SetMediaMetadata_OnMediaMetadataExtractionSuccess_MediaService), e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryLimit);
                                    cfg.Handle<AppConcurrencyException>();
                                });
                                e.ConfigureConsumer<SetMediaMetadata_OnMediaMetadataExtractionSuccess_MediaService>(context);
                            });

                            cfg.ReceiveEndpoint(nameof(SetMediaMetadata_OnMediaMetadataExtractionFailed_MediaService), e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryLimit);
                                    cfg.Handle<AppConcurrencyException>();
                                });
                                e.ConfigureConsumer<SetMediaMetadata_OnMediaMetadataExtractionFailed_MediaService>(context);
                            });

                            cfg.ConfigureEndpoints(context);
                        });
                    }
                );
    }
}
