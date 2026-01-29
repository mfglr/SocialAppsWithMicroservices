using AutoMapper;
using MediatR;
using UserQueryService.Domain;

namespace UserQueryService.Application.UseCases.UpsertUser
{
    internal class UpsertUserHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<UpsertUserRequest>
    {
        public async Task Handle(UpsertUserRequest request, CancellationToken cancellationToken)
        {
            var userVersion = await userRepository.GetByIdAsync(request.Id.ToString(), cancellationToken);
            var user = userVersion?.User;
            if (user != null && request.Version <= user.Version) return;
            if (user == null && request.IsDeleted) return;
            if (user != null && request.IsDeleted)
            {
                await userRepository.DeleteAsync(user, cancellationToken);
                return;
            }


            var media = mapper.Map<IEnumerable<UpsertUserRequest_Media>, IEnumerable<Media>>(request.Media).Where(x => !x.IsDeleted);

            if (user == null)
            {
                user = new User(
                    request.Id.ToString(),
                    request.CreatedAt,
                    request.UpdatedAt,
                    request.Version,
                    request.Name,
                    request.Username,
                    request.Gender,
                    media
                );
                await userRepository.CreateAsync(user, cancellationToken);
                return;
            }

            var version = userVersion!.Version;
            user.Set(
                request.UpdatedAt,
                request.Version,
                request.Name,
                request.Username,
                request.Gender,
                media
            );
            await userRepository.UpdateAsync(user, version, cancellationToken);
        }
    }
}
