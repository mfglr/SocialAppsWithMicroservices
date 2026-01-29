using AutoMapper;
using MassTransit;
using MediatR;
using PostService.Application.Exceptions;
using PostService.Domain;
using PostService.Domain.Exceptions;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.DeletePost
{
    internal class DeletePostHandler(IPostRepository postRepository, IMapper mapper, IIdentityService identityService, IPublishEndpoint publishEndpoint) : IRequestHandler<DeletePostRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IIdentityService _identityService = identityService;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(DeletePostRequest request, CancellationToken cancellationToken)
        {
            var post =
                 await _postRepository.GetByIdAsync(request.Id, cancellationToken) ??
                 throw new PostNotFoundException();

            if (!_identityService.IsAdminOrOwner(post.UserId))
                throw new UnauthorizedOperationException();

            post.Delete();
            await _postRepository.UpdateAsync(post, cancellationToken);

            var @event = _mapper.Map<Post, PostDeletedEvent>(post);
            await _publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}
