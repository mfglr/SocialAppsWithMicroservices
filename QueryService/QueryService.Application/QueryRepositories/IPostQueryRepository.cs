using QueryService.Application.Pagination;
using QueryService.Application.UseCases.PostUseCases;

namespace QueryService.Application.QueryRepositories
{
    public interface IPostQueryRepository
    {
        Task<PostResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<PostResponse>> GetPostsByUserId(Guid userId, Page<DateTime> page, CancellationToken cancellationToken);
    }
}
