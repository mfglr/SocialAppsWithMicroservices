using QueryService.Application.UseCases.CommentUseCases;

namespace QueryService.Application.QueryRepositories
{
    public interface ICommentQueryRepository
    {
        Task<CommentResponse?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<List<CommentResponse>> GetByPostIdAsync(Guid postId, CancellationToken cancellationToken);
    }
}
