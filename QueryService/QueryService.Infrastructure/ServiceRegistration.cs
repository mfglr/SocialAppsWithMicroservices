using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueryService.Application;
using QueryService.Domain.PostDomain;

namespace QueryService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDbContext<SqlContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")))
                .AddScoped<IPostRepository,PostRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
