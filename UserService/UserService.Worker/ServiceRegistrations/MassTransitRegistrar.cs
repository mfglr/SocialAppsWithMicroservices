using MassTransit;
using UserService.Worker.Consumers;
using UserService.Worker.ServiceRegistrations;

namespace UserService.Worker.ServiceRegistrations
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
                        brc.AddConsumer<SendEmailVerificationMailOnUserCreatedConsumer>();
                        brc.AddConsumer<SetUserMediaMetadata_OnUserMediaMetadaExtracted_UserService>();
                        brc.AddConsumer<SetUserMediaModerationResult_OnUserMediaClassfied_UserService>();

                        brc.AddConsumer<AddUserMediaThumbnail_OnUserMediaThumbnailGenerated_UserService>();

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
