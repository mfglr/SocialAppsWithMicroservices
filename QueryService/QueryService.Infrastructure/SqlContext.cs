using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using QueryService.Domain.CommentDomain;
using QueryService.Domain.PostDomain;
using QueryService.Domain.UserDomain;
using System.Reflection;

namespace QueryService.Infrastructure
{
    internal class SqlContext(DbContextOptions<SqlContext> options) : DbContext(options)
    {
        public DbSet<Post> Posts { get; private set; }
        public DbSet<Comment> Comments { get; private set; }
        public DbSet<User> Users { get; private set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }

    internal class AppDbContextFactory : IDesignTimeDbContextFactory<SqlContext>
    {
        public SqlContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SqlContext>();
            builder.UseSqlServer("Server=localhost; Database=AppQueryDB; User Id=sa; Password=123456789Abc*;TrustServerCertificate=True;");
            return new SqlContext(builder.Options);
        }
    }
}
