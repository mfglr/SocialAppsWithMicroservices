using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases.CreateUser
{
    public class CreateUserHandler(IUserRepository userRepository, IAuthService authService, IPublishEndpoint publishEndpoint, IMapper mapper) : IRequestHandler<CreateUserRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthService _authService = authService;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var username = Username.GenerateRandom();

            Guid userId = await _authService.RegisterAsync(
                username.Value,
                request.Email,
                request.Password,
                cancellationToken
            );

            var user = new User(userId, username);
            await _userRepository.CreateUserAsync(user, cancellationToken);

            var @event = _mapper.Map<User, UserCreatedEvent>(user);

            await _publishEndpoint.Publish(
                @event,
                cancellationToken
            );
        }
    }
}
