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
        private readonly IPostRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(SetPostContentModerationResultRequest request, CancellationToken cancellationToken)
        {
            var post =
                await _repository.GetByIdAsync(request.Id, cancellationToken) ??
                throw new PostNotFoundException();

            post.SetContentModerationResult(request.ModerationResult);
            await _repository.UpdateAsync(post, cancellationToken);

            var @event = _mapper.Map<Post, PostContentModerationResultSetEvent>(post);
            await _publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}
