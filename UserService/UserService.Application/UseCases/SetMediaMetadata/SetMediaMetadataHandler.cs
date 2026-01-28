using AutoMapper;
using MassTransit;
using MediatR;
using Orleans;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases.SetMediaMetadata
{
    internal class SetMediaMetadataHandler(IMapper mapper, IGrainFactory grainFactory, IPublishEndpoint publishEndpoint) : IRequestHandler<SetMediaMetadataRequest>
    {
        public async Task Handle(SetMediaMetadataRequest request, CancellationToken cancellationToken)
        {
            var userGrain = grainFactory.GetGrain<IUserGrain>(request.Id);

            var metadata = mapper.Map<Metadata>(request.Metadata);
            await userGrain.SetMediaMatadata(request.BlobName, metadata);

            var user = await userGrain.Get();
            if (user.IsPreprocessingCompleted())
            {
                var @event = mapper.Map<User, UserUpdatedEvent>(user);
                await publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
