using Microsoft.Extensions.DependencyInjection;
using VideoTranscoder.Application;

namespace VideoTranscoder.Infrastructure.FFmpegVideoTranscoder
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddFFmpegVideoTranscoder(this IServiceCollection services)
        {
            FFmpegConfigration.Configure();
            return services
                .AddScoped<IVideoTranscoder, VideoTranscoder>();
        }
    }
}
