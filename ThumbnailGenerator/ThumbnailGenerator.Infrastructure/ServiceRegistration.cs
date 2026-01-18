using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThumbnailGenerator.Infrastructure.FFmpegThumbnailGenerator;
using ThumbnailGenerator.Infrastructure.LocalBlobService;

namespace ThumbnailGenerator.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddLocalBlobService(configuration)
                .AddFFmpegThumbnailGenerator();
    }
}
