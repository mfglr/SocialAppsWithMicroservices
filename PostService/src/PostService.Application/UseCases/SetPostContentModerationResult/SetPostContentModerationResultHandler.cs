using AutoMapper;
using MassTransit;
using MediatR;
using PostService.Domain;
using PostService.Domain.Exceptions;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.SetPostContentModerationResult
{
    internal class SetPostContentModerationResultHandler(IPostRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<SetPostContentModerationResultRequest>
    {
        public async Task Handle(SetPostContentModerationResultRequest request, CancellationToken cancellationToken)
        {
            var post =
                await repository.GetByIdAsync(request.Id, cancellationToken) ??
                throw new PostNotFoundException();

            var moderationResult = mapper.Map<Shared.Events.ModerationResult, ModerationResult>(request.ModerationResult);
            post.SetContentModerationResult(moderationResult);
            await repository.UpdateAsync(post, cancellationToken);

            if (post.IsValid())
            {
                var @event = mapper.Map<Post, PostContentModerationResultSetEvent>(post);
                await publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
