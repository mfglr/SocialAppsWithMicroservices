using Microsoft.EntityFrameworkCore;
using QueryService.Domain.PostDomain;

namespace QueryService.Infrastructure
{
    internal class PostRepository(SqlContext sqlContext) : IPostRepository
    {
        private readonly SqlContext _sqlContext = sqlContext;

        public async Task CreateAsync(Post post, CancellationToken cancellationToken) =>
            await _sqlContext.Posts.AddAsync(post, cancellationToken);

        public void Delete(Post post) =>
            _sqlContext.Remove(post);

        public Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            _sqlContext.Posts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public Task<Post?> GetAsNoTrackingByIdAsync(Guid id, CancellationToken cancellationToken) =>
            _sqlContext.Posts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
