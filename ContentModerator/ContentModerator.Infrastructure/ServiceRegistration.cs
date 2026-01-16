using ContentModerator.Application;
using ContentModerator.Infrastructure.AzureAIContentModeration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ContentModerator.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(ConnectionMultiplexer.Connect(configuration["Redis:Host"]!))
                .AddSingleton<IAccessTokenProvider, RedisAccessTokenProvider>()
                .AddAzureAIContentModerationServices(configuration)
                .AddSingleton<IFrameExtractor, FrameExtractor>()
                .AddSingleton<IImageFrameExtractor, ImageFrameExtractor>()
                .AddSingleton<IVideoFrameExtractor, VideoFrameExtractor>()
                .AddSingleton<IBlobService, LocalBlobService>();
    }
}
