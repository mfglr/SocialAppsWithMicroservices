using System.Reflection;

namespace PostService.Workers.ServiceRegistrations
{
    internal static class AutoMapperRegistrar
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddAutoMapper(
                    cfg => cfg.LicenseKey = configuration["LuckPenny:LicenseKey"],
                    Assembly.GetExecutingAssembly()
                );
    }
}
