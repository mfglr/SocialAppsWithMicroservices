using MongoDB.Driver;
using PostService.Domain;

namespace PostService.Infrastructure
{
    internal class PostRepository(MongoContext context) : IPostRepository
    {
        private readonly MongoContext _context = context;

        public Task CreateAsync(Post post, CancellationToken cancellationToken)
            => _context.Posts.InsertOneAsync(post, cancellationToken: cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<Post>.Filter.Eq(c => c.Id, id);
            await _context.Posts.DeleteOneAsync(filter, cancellationToken: cancellationToken);
        }

        public async Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<Post>.Filter.Eq(c => c.Id, id);
            var documents = await _context.Posts.FindAsync(filter, cancellationToken: cancellationToken);
            return await documents.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAsync(Post post, CancellationToken cancellationToken)
        {
            var filter = Builders<Post>.Filter.And(
                Builders<Post>.Filter.Eq(c => c.Id, post.Id),
                Builders<Post>.Filter.Eq(c => c.Version, post.Version - 1)
            );
            var result = await _context.Posts.ReplaceOneAsync(filter, post, cancellationToken: cancellationToken);
            if (result.ModifiedCount == 0)
                throw new AppConcurrencyException();
        }

        public async Task UpdateAsync(IEnumerable<Post> posts, CancellationToken cancellationToken)
        {
            using var session = await _context.Client.StartSessionAsync(cancellationToken: cancellationToken);
            session.StartTransaction();

            var updates = new List<WriteModel<Post>>();
            foreach (var post in posts)
            {
                var filter = Builders<Post>.Filter.And(
                    Builders<Post>.Filter.Eq(c => c.Id, post.Id),
                    Builders<Post>.Filter.Eq(c => c.Version, post.Version - 1)
                );
                updates.Add(new ReplaceOneModel<Post>(filter, post));
            }
            var result = await _context.Posts.BulkWriteAsync(updates, cancellationToken: cancellationToken);
            if (result.ModifiedCount < posts.Count())
                throw new AppConcurrencyException();

            await session.CommitTransactionAsync(cancellationToken);
        }
    }
}
