using Microsoft.EntityFrameworkCore;
using QueryService.Domain.CommentDomain;

namespace QueryService.Infrastructure
{
    internal class CommentRepository(SqlContext context) : ICommentRepository
    {
        private readonly SqlContext _context = context;

        public async Task CreateAsync(Comment comment, CancellationToken cancellationToken) =>
            await _context.Comments.AddAsync(comment, cancellationToken);

        public void Delete(Comment comment) =>
            _context.Comments.Remove(comment);

        public Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            _context.Comments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
