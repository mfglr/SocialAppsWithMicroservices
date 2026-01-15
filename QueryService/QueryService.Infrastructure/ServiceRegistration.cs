using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueryService.Application;
using QueryService.Domain.CommentDomain;
using QueryService.Domain.PostDomain;
using QueryService.Domain.UserDomain;

namespace QueryService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDbContext<SqlContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")))
                .AddScoped<IPostRepository,PostRepository>()
                .AddScoped<ICommentRepository,CommentRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
