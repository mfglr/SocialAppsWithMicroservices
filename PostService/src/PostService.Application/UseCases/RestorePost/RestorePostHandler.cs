using AutoMapper;
using MassTransit;
using MediatR;
using PostService.Domain;
using PostService.Domain.Exceptions;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.RestorePost
{
    internal class RestorePostHandler(IPostRepository postRepository, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<RestorePostRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(RestorePostRequest request, CancellationToken cancellationToken)
        {
            var post =
               await _postRepository.GetByIdAsync(request.Id, cancellationToken) ??
               throw new PostNotFoundException();
            post.Restore();
            await _postRepository.UpdateAsync(post, cancellationToken);
            
            var @event = _mapper.Map<Post, PostRestoredEvent>(post);
            await _publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}
