using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoTranscoder.Infrastructure.FFmpegVideoTranscoder;
using VideoTranscoder.Infrastructure.LocalBlobService;

namespace VideoTranscoder.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddLocalBlobService(configuration)
                .AddFFmpegVideoTranscoder();
    }
}
