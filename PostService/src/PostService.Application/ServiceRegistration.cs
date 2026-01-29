using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PostService.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuraiton)
        {
            return services
                .AddAutoMapper(
                    cfg => cfg.LicenseKey = configuraiton.GetSection("LuckPenny:LicenseKey").Value,
                    Assembly.GetExecutingAssembly()
                )
                .AddMediatR(
                    cfg =>
                    {
                        cfg.LicenseKey = configuraiton.GetSection("LuckPenny:LicenseKey").Value;
                        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    }
                );
        }
    }
}
