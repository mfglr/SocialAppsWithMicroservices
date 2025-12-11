namespace CommentService.Domain
{
    public interface ICommentRepository
    {
        Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task CreateAsync(Comment comment, CancellationToken cancellationToken);
        Task UpdateAsync(Comment comment, CancellationToken cancellationToken);
        Task DeleteAsync(Comment comment, CancellationToken cancellationToken);
    }
}
