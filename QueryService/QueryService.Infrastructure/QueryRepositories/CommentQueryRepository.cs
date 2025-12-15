using Microsoft.EntityFrameworkCore;
using QueryService.Application.QueryRepositories;
using QueryService.Application.UseCases.CommentUseCases;

namespace QueryService.Infrastructure.QueryRepositories
{
    internal class CommentQueryRepository(SqlContext context) : ICommentQueryRepository
    {
        private readonly SqlContext _context = context;

        public Task<CommentResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            _context.Comments
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ToCommentResponse()
                .FirstOrDefaultAsync(cancellationToken);

        public Task<List<CommentResponse>> GetByPostIdAsync(Guid postId, CancellationToken cancellationToken) =>
            _context.Comments
                .AsNoTracking()
                .Where(x => x.PostId == postId)
                .ToCommentResponse()
                .ToListAsync(cancellationToken);
    }
}
