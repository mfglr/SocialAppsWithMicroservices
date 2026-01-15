using AutoMapper;
using MassTransit;
using MediatR;
using PostService.Application.Exceptions;
using PostService.Domain;
using PostService.Domain.Exceptions;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.DeletePostMedia
{
    internal class DeletePostMediaHandler(IPostRepository postRepository, IMapper mapper, IIdentityService identityService, IPublishEndpoint publishEndpoint) : IRequestHandler<DeletePostMediaRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IIdentityService _identityService = identityService;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;


        public async Task Handle(DeletePostMediaRequest request, CancellationToken cancellationToken)
        {
            var post =
                await _postRepository.GetByIdAsync(request.Id, cancellationToken) ??
                throw new PostNotFoundException();

            if (_identityService.UserId != post.UserId)
                throw new UnauthorizedOperationException();

            post.DeleMedia(request.BlobName);
            await _postRepository.UpdateAsync(post, cancellationToken);

            var @event = _mapper.Map<Post, PostMediaDeletedEvent>(post);
            await _publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}
