using MetadataExtractor.Infrastructure.FFmpegMetadataExtractor;
using MetadataExtractor.Infrastructure.LocalBlobService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MetadataExtractor.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddLocalBlobService(configuration)
                .AddFFmpegMetadataExtractor();
    }
}
