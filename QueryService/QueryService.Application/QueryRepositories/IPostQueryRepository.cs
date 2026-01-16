using QueryService.Application.UseCases.PostUseCases;

namespace QueryService.Application.QueryRepositories
{
    public interface IPostQueryRepository
    {
        Task<PostResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
