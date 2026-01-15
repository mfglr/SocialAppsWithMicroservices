using Microsoft.EntityFrameworkCore;
using QueryService.Domain.UserDomain;

namespace QueryService.Infrastructure
{
    internal class UserRepository(SqlContext context) : IUserRepository
    {

        private readonly SqlContext _context = context;

        public async Task CreateAsync(User user, CancellationToken cancellationToken) =>
            await _context.Users.AddAsync(user, cancellationToken);

        public void Delete(User user) =>
            _context.Users.Remove(user);

        public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            _context.Users.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
    }
}
