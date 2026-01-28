using AutoMapper;
using MassTransit;
using MediatR;
using Orleans;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases.AddMediaThumbnail
{
    internal class AddMediaThumbnailHandler(IMapper mapper, IPublishEndpoint publishEndpoint, IGrainFactory grainFactory) : IRequestHandler<AddMediaThumbnailRequest>
    {
        public async Task Handle(AddMediaThumbnailRequest request, CancellationToken cancellationToken)
        {
            var userGrain = grainFactory.GetGrain<IUserGrain>(request.Id);

            var tumbnail = mapper.Map<Shared.Objects.Thumbnail, Thumbnail>(request.Thumbnail);
            await userGrain.AddMediaThumbnail(request.BlobName, tumbnail);

            var user = await userGrain.Get();
            if (user.IsPreprocessingCompleted())
            {
                var @event = mapper.Map<User, UserUpdatedEvent>(user);
                await publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
