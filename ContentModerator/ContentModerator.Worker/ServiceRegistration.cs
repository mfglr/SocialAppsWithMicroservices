using ContentModerator.Worker;
using ContentModerator.Worker.Consumers.CommentDomain;
using ContentModerator.Worker.Consumers.PostDomain;
using ContentModerator.Worker.Consumers.UserDomain;
using MassTransit;

namespace ContentModerator.Worker
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configration) =>
            services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<ClassifyPostMedia_OnPostMediaMetadataExtracted_ContentModerator>();
                    x.AddConsumer<ClassifyPostContent_OnPostCreated_ContentModerator>();
                    x.AddConsumer<ClassifyPostContent_OnPostContentUpdated_ContentModerator>();

                    x.AddConsumer<ClassifyImage_OnUserMediaCreated_ContentModerator>();

                    x.AddConsumer<ClassifyCommentContent_OnCommentCreated_ContentModerator>();
                    x.AddConsumer<ClassifyCommentContent_OnContentUpdated_ContentModerator>();

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
