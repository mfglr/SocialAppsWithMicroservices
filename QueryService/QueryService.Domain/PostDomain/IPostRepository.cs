namespace QueryService.Domain.PostDomain
{
    public interface IPostRepository
    {
        Task CreateAsync(Post post, CancellationToken cancellationToken);
        void Delete(Post post);
        Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
