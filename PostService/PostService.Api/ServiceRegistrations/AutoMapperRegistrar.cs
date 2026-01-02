using PostService.Application;
using System.Reflection;

namespace PostService.Api.ServiceRegistrations
{
    internal class AutoMapperOptions
    {
        public required string LicenseKey { get; set; }
    }

    internal static class AutoMapperRegistrar
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var option = configuration.GetSection(nameof(AutoMapperOptions)).Get<AutoMapperOptions>()!;
            return services
                .AddAutoMapper(
                    cfg => {
                        cfg.LicenseKey = option.LicenseKey;
                    },
                    Assembly.GetExecutingAssembly(),
                    Assembly.GetAssembly(typeof(IBlobService))
                );
        }
            
    }
}
