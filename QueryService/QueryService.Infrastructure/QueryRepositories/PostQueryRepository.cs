using Microsoft.EntityFrameworkCore;
using QueryService.Application.QueryRepositories;
using QueryService.Application.UseCases.PostUseCases;

namespace QueryService.Infrastructure.QueryRepositories
{
    internal class PostQueryRepository(SqlContext context) : IPostQueryRepository
    {
        private readonly SqlContext _context = context;

        public Task<PostResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            _context
                .Posts
                .AsNoTracking()
                .ToPostResponse(_context)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
