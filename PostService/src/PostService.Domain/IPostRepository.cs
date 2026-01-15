namespace PostService.Domain
{
    public interface IPostRepository
    {
        Task CreateAsync(Post post, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Post question, CancellationToken cancellationToken);
        Task UpdateAsync(IEnumerable<Post> posts, CancellationToken cancellationToken);
    }
}
