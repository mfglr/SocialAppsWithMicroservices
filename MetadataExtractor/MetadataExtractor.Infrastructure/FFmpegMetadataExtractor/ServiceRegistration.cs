using MetadataExtractor.Application;
using Microsoft.Extensions.DependencyInjection;

namespace MetadataExtractor.Infrastructure.FFmpegMetadataExtractor
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddFFmpegMetadataExtractor(this IServiceCollection services)
        {
            FFmpegConfigration.Configure();
            return services
                .AddScoped<TempDirectoryManager>()
                .AddScoped<IMetadataExtractor, FFmpegMetadataExtractor>();
        }
            
    }
}
