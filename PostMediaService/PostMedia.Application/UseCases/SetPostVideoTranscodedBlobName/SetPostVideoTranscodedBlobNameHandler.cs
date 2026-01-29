using AutoMapper;
using MassTransit;
using MediatR;
using PostMedia.Domain;
using Shared.Events.PostMediaService;

namespace PostMedia.Application.UseCases.SetPostVideoTranscodedBlobName
{
    internal class SetPostVideoTranscodedBlobNameHandler(IGrainFactory grainFactory, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<SetPostVideoTranscodedBlobNameRequest>
    {
        public async Task Handle(SetPostVideoTranscodedBlobNameRequest request, CancellationToken cancellationToken)
        {
            var grain = grainFactory.GetGrain<IPostGrain>(request.Id);
            var post = await grain.SetTranscodedBlobName(request.BlobName, request.TranscodedBlobName);

            if (post.IsPreprocessingCompleted())
            {
                var @event = mapper.Map<Post, PostMediaPreproccessingCompletedEvent>(post);
                await publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
