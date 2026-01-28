using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases.UpdateName
{
    internal class UpdateNameHandler(IGrainFactory grainFactory, IPublishEndpoint publishEndpoint, IMapper mapper, IIdentityService identityService) : IRequestHandler<UpdateNameRequest>
    {
        public async Task Handle(UpdateNameRequest request, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId;
            var name = new Name(request.Name);
            var userGrain = grainFactory.GetGrain<IUserGrain>(userId);
            await userGrain.UpdateName(name);
            var user = await userGrain.Get();
            var @event = mapper.Map<User, NameUpdatedEvent>(user);
            await publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}
