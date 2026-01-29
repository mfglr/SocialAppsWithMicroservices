using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PostMedia.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddMediatR(cfg => {
                    cfg.LicenseKey = configuration["LuckPenny:LicenseKey"];
                    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                })
                .AddAutoMapper(
                    cfg => cfg.LicenseKey = cfg.LicenseKey = configuration["LuckPenny:LicenseKey"],
                    Assembly.GetExecutingAssembly()
                );
    }
}
