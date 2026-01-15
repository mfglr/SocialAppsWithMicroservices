namespace QueryService.Domain.UserDomain
{
    public interface IUserRepository
    {
        Task CreateAsync(User user, CancellationToken cancellationToken);
        void Delete(User user);
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
