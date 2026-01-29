using AutoMapper;
using MassTransit;
using MediatR;
using PostMedia.Domain;
using Shared.Events.PostMediaService;

namespace PostMedia.Application.UseCases.SetPostMediaMetadata
{
    internal class SetPostMediaMetadataHandler(IGrainFactory grainFactory, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<SetPostMediaMetadataRequest>
    {
        public async Task Handle(SetPostMediaMetadataRequest request, CancellationToken cancellationToken)
        {
            var grain = grainFactory.GetGrain<IPostGrain>(request.Id);
            var metadata = mapper.Map<Shared.Events.Metadata, Metadata>(request.Metadata);
            var post = await grain.SetMetadata(request.BlobName, metadata);

            if (post.IsPreprocessingCompleted())
            {
                var @event = mapper.Map<Post, PostMediaPreproccessingCompletedEvent>(post);
                await publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
