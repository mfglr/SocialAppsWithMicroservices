namespace PostQueryService.Domain
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Post>> GetByUserId(Guid userId, CancellationToken cancellationToken);

        Task CreateAsync(Post post, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Post post, CancellationToken cancellationToken);
        Task UpdateAsync(IEnumerable<Post> posts, CancellationToken cancellationToken);
    }
}
