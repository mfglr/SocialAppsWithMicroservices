using MediatR;
using UserQueryService.Domain;

namespace UserQueryService.Application.UseCases.UpsertUser
{
    internal class UpsertUserHandler(IUserRepository userRepository) : IRequestHandler<UpsertUserRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task Handle(UpsertUserRequest request, CancellationToken cancellationToken)
        {
            var userVersion = await _userRepository.GetByIdAsync(request.Id.ToString(), cancellationToken);
            var user = userVersion?.User;
            if (user != null && request.Version <= user.Version) return;
            if (user == null && request.IsDeleted) return;
            if (user != null && request.IsDeleted)
            {
                await _userRepository.DeleteAsync(user, cancellationToken);
                return;
            }
            
            if(user == null)
            {
                user = new User(
                    request.Id.ToString(),
                    request.CreatedAt,
                    request.UpdatedAt,
                    request.Version,
                    request.Name,
                    request.Username,
                    request.Gender,
                    request.Media
                );
                await _userRepository.CreateAsync(user, cancellationToken);
                return;
            }

            var version = userVersion!.Version;
            user.Set(
                request.UpdatedAt,
                request.Version,
                request.Name,
                request.Username,
                request.Gender,
                request.Media
            );
            await _userRepository.UpdateAsync(user, version, cancellationToken);
        }
    }
}
