using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediaService.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddMediatR(cfg =>
                {
                    cfg.LicenseKey = configuration["LuckPenny:LicenseKey"]!;
                    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                })
                .AddAutoMapper(
                    cfg => {
                        cfg.LicenseKey = configuration["LuckPenny:LicenseKey"]!;
                    },
                    Assembly.GetExecutingAssembly()
                );
    }
}
