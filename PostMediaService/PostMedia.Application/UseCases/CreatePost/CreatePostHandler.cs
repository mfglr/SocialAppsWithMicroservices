using AutoMapper;
using MassTransit;
using MediatR;
using PostMedia.Domain;
using Shared.Events.PostMediaService;

namespace PostMedia.Application.UseCases.CreatePost
{
    internal class CreatePostHandler(IGrainFactory grainFactory,IPublishEndpoint publishEndpoint, IMapper mapper) : IRequestHandler<CreatePostRequest>
    {
        public async Task Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var postGrain = grainFactory.GetGrain<IPostGrain>(request.Id);
            var media = mapper.Map<IEnumerable<CreatePostRequest_Media>, IEnumerable<Media>>(request.Media);
            await postGrain.Create(media);
            
            var events = request.Media.Select(x => new PostMediaCreatedEvent(request.Id, x.ContainerName, x.BlobName, x.Type));
            await publishEndpoint.PublishBatch(events, cancellationToken);
        }
    }
}
