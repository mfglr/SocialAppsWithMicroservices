using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UserQueryService.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddAutoMapper(
                    cfg => cfg.LicenseKey = configuration["LuckPenny:LicenseKey"],
                    Assembly.GetExecutingAssembly()
                )
                .AddMediatR(cfg =>
                {
                    cfg.LicenseKey = configuration["LuckPenny:LicenseKey"];
                    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                });
    }
}
