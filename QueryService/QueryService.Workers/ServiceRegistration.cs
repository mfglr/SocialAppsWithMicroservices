using MassTransit;
using Microsoft.EntityFrameworkCore;
using QueryService.Workers;
using QueryService.Workers.Consumers.CommentDomain.DeleteComment;
using QueryService.Workers.Consumers.CommentDomain.RestoreComment;
using QueryService.Workers.Consumers.CommentDomain.SetCommentContentModerationResult;
using QueryService.Workers.Consumers.PostDomain;
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
                        cfg.LicenseKey = configuration["LuckPenny:LicenseKey"]!;
                    },
                    Assembly.GetExecutingAssembly(),
                    Assembly.GetAssembly(typeof(Application.ServiceRegistration))
                );

        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMassTransit(
                x =>
                {
                    //post consumers
                    x.AddConsumer<CreatePost_OnPostCreated_QueryService>();//+1 parallel operations
                    x.AddConsumer<SetPostMedia_OnPostMediaSet_QueryService>();//+5 parallel operations
                    x.AddConsumer<SetPostContentModerationResult_OnPostContentModerationResultSet_QueryService>();//+1 parallel operations
                    x.AddConsumer<UpdatePostContent_OnPostContentUpdated_QueryService>();
                    x.AddConsumer<DeletePostMedia_OnPostMediaDeleted_QueryService>();
                    x.AddConsumer<DeletePost_OnPostDeleted_QueryService>();
                    x.AddConsumer<RestorePost_OnPostRestored_QueryService>();
                    //post consumers

                    //comment consumers
                    x.AddConsumer<SetCommentContentModerationResultConsumer_QueryService>();
                    x.AddConsumer<DeleteCommentConsumer_QueryService>();
                    x.AddConsumer<RestoreCommentConsumer_QueryService>();
                    //comment consumers

                    //user consumers
                    //x.AddConsumer<CreateUserConsumer_QueryService>();
                    //user consumers

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(configuration["RabbitMQ:Host"], configuration["RabbitMQ:VirtualHost"], h =>
                        {
                            h.Username(configuration["RabbitMQ:UserName"]!);
                            h.Password(configuration["RabbitMQ:Password"]!);
                        });

                        //post consumers
                        var countOfParallelOperations = 7;
                        cfg.ReceiveEndpoint(
                            nameof(CreatePost_OnPostCreated_QueryService),
                            e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(countOfParallelOperations - 1);
                                    cfg.Handle<DbUpdateConcurrencyException>();
                                    cfg.Handle<DbUpdateException>();
                                });
                                e.ConfigureConsumer<CreatePost_OnPostCreated_QueryService>(context);
                            }
                        );
                        cfg.ReceiveEndpoint(
                            nameof(SetPostMedia_OnPostMediaSet_QueryService),
                            e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(countOfParallelOperations - 1);
                                    cfg.Handle<DbUpdateConcurrencyException>();
                                    cfg.Handle<DbUpdateException>();

                                });
                                e.ConfigureConsumer<SetPostMedia_OnPostMediaSet_QueryService>(context);
                            }
                        );
                        cfg.ReceiveEndpoint(
                            nameof(SetPostContentModerationResult_OnPostContentModerationResultSet_QueryService),
                            e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(countOfParallelOperations - 1);
                                    cfg.Handle<DbUpdateConcurrencyException>();
                                    cfg.Handle<DbUpdateException>();
                                });
                                e.ConfigureConsumer<SetPostContentModerationResult_OnPostContentModerationResultSet_QueryService>(context);
                            }
                        );

                        var retryCount = 5;
                        cfg.ReceiveEndpoint(
                            nameof(UpdatePostContent_OnPostContentUpdated_QueryService),
                            e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryCount);
                                    cfg.Handle<DbUpdateConcurrencyException>();
                                    cfg.Handle<DbUpdateException>();
                                });
                                e.ConfigureConsumer<UpdatePostContent_OnPostContentUpdated_QueryService>(context);
                            }
                        );
                        cfg.ReceiveEndpoint(
                            nameof(DeletePostMedia_OnPostMediaDeleted_QueryService),
                            e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryCount);
                                    cfg.Handle<DbUpdateConcurrencyException>();
                                    cfg.Handle<DbUpdateException>();
                                });
                                e.ConfigureConsumer<DeletePostMedia_OnPostMediaDeleted_QueryService>(context);
                            }
                        );
                        cfg.ReceiveEndpoint(
                            nameof(DeletePost_OnPostDeleted_QueryService),
                            e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryCount);
                                    cfg.Handle<DbUpdateConcurrencyException>();
                                    cfg.Handle<DbUpdateException>();
                                });
                                e.ConfigureConsumer<DeletePost_OnPostDeleted_QueryService>(context);
                            }
                        );
                        cfg.ReceiveEndpoint(
                            nameof(RestorePost_OnPostRestored_QueryService),
                            e =>
                            {
                                e.UseMessageRetry(cfg =>
                                {
                                    cfg.Immediate(retryCount);
                                    cfg.Handle<DbUpdateConcurrencyException>();
                                    cfg.Handle<DbUpdateException>();
                                });
                                e.ConfigureConsumer<RestorePost_OnPostRestored_QueryService>(context);
                            }
                        );
                        //post consumers

                        cfg.ConfigureEndpoints(context);
                    });
                }
            );
    }
}
