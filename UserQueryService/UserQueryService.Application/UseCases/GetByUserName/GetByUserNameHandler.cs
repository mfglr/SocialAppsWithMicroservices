using MediatR;
using UserQueryService.Application.Exceptions;

namespace UserQueryService.Application.UseCases.GetByUserName
{
    internal class GetByUserNameHandler(IUserQueryRepository userQueryRepository) : IRequestHandler<GetByUserNameRequest, UserResponse>
    {
        private readonly IUserQueryRepository _userQueryRepository = userQueryRepository;

        public async Task<UserResponse> Handle(GetByUserNameRequest request, CancellationToken cancellationToken)
        {
            var user = 
                await _userQueryRepository.GetByUserNameAsync(request.UserName.ToLower().Trim(), cancellationToken) ?? 
                throw new UserNotFoundException();

            return user;
        }
    }
}
