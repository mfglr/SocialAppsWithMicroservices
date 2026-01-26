using UserQueryService.Application.UseCases;

namespace UserQueryService.Application
{
    public interface IUserQueryRepository
    {
        Task<UserResponse?> GetByUserNameAsync(string userName, CancellationToken cancelToken);
    }
}
