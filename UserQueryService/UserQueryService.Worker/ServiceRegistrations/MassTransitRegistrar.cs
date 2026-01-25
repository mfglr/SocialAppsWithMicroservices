using MassTransit;
using UserQueryService.Worker.Consumers;
using UserQueryService.Worker.ServiceRegistrations;

namespace UserQueryService.Worker.ServiceRegistrations
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
            return services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<CreateUser_OnUserCreated_UserQueryService>();
                    x.AddConsumer<UpdateName_OnNameUpdated_UserQueryService>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(option.Host, option.VirtualHost, h =>
                        {
                            h.Username(option.UserName);
                            h.Password(option.Password);
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                }
            );
        }
    }
}
