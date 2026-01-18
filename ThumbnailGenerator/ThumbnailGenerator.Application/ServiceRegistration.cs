using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ThumbnailGenerator.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped<TempDirectoryManager>()
                .AddMediatR(
                    cfg =>
                    {
                        cfg.LicenseKey = configuration["LuckPenny:LicenseKey"];
                        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    }
                );
    }
}
