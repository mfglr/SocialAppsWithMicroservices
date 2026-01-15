using PostService.Domain;

namespace PostService.Application
{
    public interface IPostCacheService
    {
        Task<Post?> GetByIdAsync(Guid id);
        Task CreateAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(Post post);
    }
}
