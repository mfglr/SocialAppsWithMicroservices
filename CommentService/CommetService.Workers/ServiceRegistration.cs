using CommetService.Workers;
using CommetService.Workers.Consumers.DeletePostCommentsOnPostDeleted;
using CommetService.Workers.Consumers.DeleteRepliesOnCommentDeleted;
using CommetService.Workers.Consumers.RestorePostCommentsOnPostRestored;
using CommetService.Workers.Consumers.RestoreRepliesOnCommentRestored;
using CommetService.Workers.Consumers.SetCommentContentModerationResult;
using MassTransit;
using System.Reflection;

namespace CommetService.Workers
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddAutoMapper(
                    cfg => {
                        cfg.LicenseKey = configuration["AutoMapper:LicenseKey"]!;
                    },
                    Assembly.GetExecutingAssembly(),
                    Assembly.GetAssembly(typeof(CommentService.Application.ServiceRegistration))
                );

        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<SetCommentContentModerationResult_CommentService>();
                    x.AddConsumer<DeleteRepliesOnCommentDeleted>();
                    x.AddConsumer<RestoreRepliesOnCommentRestored>();
                    x.AddConsumer<DeletePostCommentsOnPostDeleted>();
                    x.AddConsumer<RestorePostCommentsOnPostRestored>();

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
