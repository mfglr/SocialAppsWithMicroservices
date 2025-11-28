using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostService.Application.UseCases.CreatePost;
using PostService.Application.UseCases.SetContentModerationResult;
using PostService.Application.UseCases.SetMedia;
using System.Reflection;

namespace PostService.Application
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddAutoMapper(
                    cfg => {
                        cfg.LicenseKey = configuration["AutoMapper:LicenseKey"]!;
                    },
                    Assembly.GetExecutingAssembly()
                );

        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services
                .AddMediator(cfg =>
                {
                    cfg.AddConsumer<CreatePostConsumer>();
                    cfg.AddConsumer<SetContentModerationResultConsumer>();
                    cfg.AddConsumer<SetMediaConsumer>();
                });
    }
}
