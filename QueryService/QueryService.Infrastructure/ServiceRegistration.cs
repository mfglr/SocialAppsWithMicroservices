using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueryService.Application;
using QueryService.Domain.CommentDomain;
using QueryService.Domain.PostDomain;
using QueryService.Domain.UserDomain;
using QueryService.Infrastructure.QueryRepositories;
using System.Reflection;

namespace QueryService.Infrastructure
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddAutoMapper(
                    cfg => cfg.LicenseKey = configuration["LuckPenny:LicenseKey"],
                    Assembly.GetExecutingAssembly()
                )
                .AddDbContext<SqlContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")))
                .AddQueryRepositories()
                .AddScoped<IPostRepository,PostRepository>()
                .AddScoped<ICommentRepository,CommentRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
