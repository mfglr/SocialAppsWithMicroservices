using MassTransit;
using QueryService.Workers;
using QueryService.Workers.Consumers.CommentDomain.DeleteComment;
using QueryService.Workers.Consumers.CommentDomain.RestoreComment;
using QueryService.Workers.Consumers.CommentDomain.SetCommentContentModerationResult;
using QueryService.Workers.Consumers.PostDomain.DeletePost;
using QueryService.Workers.Consumers.PostDomain.DeletePostMedia;
using QueryService.Workers.Consumers.PostDomain.RestorePost;
using QueryService.Workers.Consumers.PostDomain.SetPostContentModerationResult;
using QueryService.Workers.Consumers.PostDomain.SetPostMedia;
using QueryService.Workers.Consumers.UserDomain.CreateUser;
using System.Reflection;

namespace QueryService.Workers
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
                    Assembly.GetAssembly(typeof(Application.ServiceRegistration))
                );

        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configration) =>
            services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<SetPostContentModerationResult_QueryService>();
                    x.AddConsumer<SetPostMedia_QueryService>();
                    x.AddConsumer<DeletePostMedia_QueryService>();
                    x.AddConsumer<DeletePost_QueryService>();
                    x.AddConsumer<RestorePost>();

                    x.AddConsumer<SetCommentContentModerationResult_QueryService>();
                    x.AddConsumer<DeleteComment>();
                    x.AddConsumer<RestoreComment>();

                    x.AddConsumer<CreateUserConsumer_QueryService>();

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
