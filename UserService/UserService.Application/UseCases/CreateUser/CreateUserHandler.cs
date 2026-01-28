using AutoMapper;
using MassTransit;
using MediatR;
using Orleans;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases.CreateUser
{
    public class CreateUserHandler(IMapper mapper, IGrainFactory grainFactory, IAuthService authService, IPublishEndpoint publishEndpoint) : IRequestHandler<CreateUserRequest>
    {
        public async Task Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var username = Username.GenerateRandom();
            Guid userId = await authService.RegisterAsync(
                username.Value,
                request.Email,
                request.Password,
                cancellationToken
            );

            var userGrain = grainFactory.GetGrain<IUserGrain>(userId);
            await userGrain.Create(username);

            var user = await userGrain.Get();
            var @event = mapper.Map<User, UserCreatedEvent>(user);
            await publishEndpoint.Publish(@event,cancellationToken);
        }
    }
}
