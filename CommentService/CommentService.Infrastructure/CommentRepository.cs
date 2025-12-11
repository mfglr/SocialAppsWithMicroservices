using CommentService.Domain;
using MongoDB.Driver;

namespace CommentService.Infrastructure
{
    internal class CommentRepository(MongoContext context) : ICommentRepository
    {
        private readonly MongoContext _context = context;

        public async Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<Comment>.Filter.Eq(c => c.Id, id);
            var documents = await _context.Comments.FindAsync(filter, cancellationToken: cancellationToken);
            return await documents.FirstOrDefaultAsync(cancellationToken);
        }

        public Task CreateAsync(Comment comment, CancellationToken cancellationToken) =>
            _context.Comments.InsertOneAsync(comment, cancellationToken: cancellationToken);

        public async Task DeleteAsync(Comment comment, CancellationToken cancellationToken)
        {
            var filter = Builders<Comment>.Filter.Eq(c => c.Id, comment.Id);
            await _context.Comments.DeleteOneAsync(filter, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(Comment comment, CancellationToken cancellationToken)
        {
            var filter = Builders<Comment>.Filter.And(
                Builders<Comment>.Filter.Eq(c => c.Id, comment.Id),
                Builders<Comment>.Filter.Eq(c => c.Version, comment.Version - 1)
            );
            var result = await _context.Comments.ReplaceOneAsync(filter, comment, cancellationToken: cancellationToken);
            if (result.ModifiedCount == 0)
                throw new AppConcurrencyException();
        }
    }
}
