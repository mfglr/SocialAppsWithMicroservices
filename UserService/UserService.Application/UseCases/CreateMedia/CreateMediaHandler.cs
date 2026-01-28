using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases.CreateMedia
{
    internal class CreateMediaHandler(IMapper mapper, IBlobService blobService, IGrainFactory grainFactory, IPublishEndpoint publishEndpoint, MediaTypeExtractor mediaTypeExtractor, IIdentityService idendityService) : IRequestHandler<CreateMediaRequest>
    {

        public async Task Handle(CreateMediaRequest request, CancellationToken cancellationToken)
        {
            var userId = idendityService.UserId;
            var userGrain = grainFactory.GetGrain<IUserGrain>(userId);
            
            string? blobName = null;
            try
            {
                var type = mediaTypeExtractor.Extract(request.Media);
                blobName = await blobService.UploadAsync(User.MediaContainerName, request.Media, cancellationToken);
                var media = new Media(blobName);
                await userGrain.AddMedia(media);
                
                var sharedMedia = mapper.Map<Media, Shared.Objects.Media>(media);
                var @event = new UserMediaCreatedEvent(userId, sharedMedia);
                await publishEndpoint.Publish(@event, cancellationToken);
            }
            catch (Exception)
            {
                if(blobName != null)
                    await blobService.DeleteAsync(User.MediaContainerName, blobName, cancellationToken);
                throw;
            }
        }
    }
}
