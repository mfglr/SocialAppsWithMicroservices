using AutoMapper;
using MassTransit;
using MediatR;
using PostMedia.Domain;
using Shared.Events.PostMediaService;

namespace PostMedia.Application.UseCases.AddPostMediaThumbnail
{
    internal class AddPostMediaThumbnailHandler(IGrainFactory grainFactory, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<AddPostMediaThumbnailRequest>
    {
        public async Task Handle(AddPostMediaThumbnailRequest request, CancellationToken cancellationToken)
        {
            var grain = grainFactory.GetGrain<IPostGrain>(request.Id);
            var thumbnail = mapper.Map<Shared.Events.Thumbnail, Thumbnail>(request.Thumbnail);
            var post = await grain.AddThumbnail(request.BlobName, thumbnail);

            if (post.IsPreprocessingCompleted())
            {
                var @event = mapper.Map<Post, PostMediaPreproccessingCompletedEvent>(post);
                await publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
