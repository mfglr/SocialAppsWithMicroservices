using AutoMapper;
using MassTransit;
using MediatR;
using PostMedia.Domain;
using Shared.Events.PostMediaService;

namespace PostMedia.Application.UseCases.SetPostMediaModerationResult
{
    internal class SetPostMediaModerationResultHandler(IGrainFactory grainFactory, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<SetPostMediaModerationResultRequest>
    {
        public async Task Handle(SetPostMediaModerationResultRequest request, CancellationToken cancellationToken)
        {
            var grain = grainFactory.GetGrain<IPostGrain>(request.Id);
            var result = mapper.Map<Shared.Events.ModerationResult, ModerationResult>(request.ModerationResult);
            var post = await grain.SetModerationResult(request.BlobName, result);

            if (post.IsPreprocessingCompleted())
            {
                var @event = mapper.Map<Post, PostMediaPreproccessingCompletedEvent>(post);
                await publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
