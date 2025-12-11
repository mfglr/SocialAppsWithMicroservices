namespace QueryService.Domain.CommentDomain
{
    public interface ICommentRepository
    {
        Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task CreateAsync(Comment comment, CancellationToken cancellationToken);
        void Delete(Comment comment);
    }
}
