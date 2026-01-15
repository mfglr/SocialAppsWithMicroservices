using AutoMapper;
using MassTransit;
using MediatR;
using PostService.Domain;
using PostService.Domain.Exceptions;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.SetPostMedia
{
    internal class SetPostMediaHandler(IPostRepository postRepository, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<SetPostMediaRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(SetPostMediaRequest request, CancellationToken cancellationToken)
        {
            var post =
                await _postRepository.GetByIdAsync(request.OwnerId, cancellationToken) ??
                throw new PostNotFoundException();

            post.SetMedia(
                request.BlobName,
                request.TranscodedBlobName,
                request.Metadata,
                request.ModerationResult,
                request.Thumbnails
            );
            await _postRepository.UpdateAsync(post, cancellationToken);

            var @event = _mapper.Map<Post, PostMediaSetEvent>(post);
            await _publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}
