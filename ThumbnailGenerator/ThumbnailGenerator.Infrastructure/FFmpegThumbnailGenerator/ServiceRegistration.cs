using Microsoft.Extensions.DependencyInjection;
using ThumbnailGenerator.Application;

namespace ThumbnailGenerator.Infrastructure.FFmpegThumbnailGenerator
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddFFmpegThumbnailGenerator(this IServiceCollection services)
        {
            FFmpegConfigration.Configure();
            return services
                .AddScoped<IThumbnailGenerator, ThumbnailGenerator>();
        }
    }
}
