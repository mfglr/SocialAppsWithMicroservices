using MassTransit;
using PostMedia.Worker.Consumers;
using PostMedia.Worker.ServiceRegistrations;

namespace PostMedia.Worker.ServiceRegistrations
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
            return services
                .AddMassTransit(
                    brc =>
                    {
                        brc.AddConsumer<CreatePostMedia_OnPostCreated_PostMediService>();
                        brc.AddConsumer<SetPostMediaMetadata_OnPostMediaMetadataExtracted_PostMediaService>();
                        brc.AddConsumer<SetPostMediaModerationResult_OnPostMediaClassifed_PostMediaService>();
                        brc.AddConsumer<AddPostMediaThumbnail_OnPostMediaThumbnailGenerated_PostMediaService>();
                        brc.AddConsumer<SetTranscodedBlobName_OnPostVideoTranscoded_PostMediaService>();
                        brc.AddConsumer<DeletePost_OnPostPreproccessingCompleted_PostMediaService>();

                        brc.UsingRabbitMq((context, rbgc) =>
                        {
                            rbgc.Host(
                                option.Host,
                                option.VirtualHost,
                                rhc =>
                                {
                                    rhc.Username(option.UserName);
                                    rhc.Password(option.Password);
                                }
                            );
                            
                            rbgc.ConfigureEndpoints(context);
                        });
                    }
                );
        }
    }
}
