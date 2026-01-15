using AutoMapper;
using MassTransit;
using MediatR;
using PostService.Application.Exceptions;
using PostService.Application.UseCases.CreatePost;
using PostService.Domain;
using PostService.Domain.Exceptions;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.CreatePostMedia
{
    internal class CreatePostMediaHandler(IBlobService blobService, IPostRepository postRepository, IIdentityService identityService, IPublishEndpoint publishEndpoint, IMapper mapper) : IRequestHandler<CreatePostMediaRequest>
    {
        private readonly IBlobService _blobService = blobService;
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IIdentityService _identityService = identityService;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(CreatePostMediaRequest request, CancellationToken cancellationToken)
        {
            var types = CreatePostMediaHelpers.GetMediaTypes(request.Media);

            var post =
                await _postRepository.GetByIdAsync(request.Id, cancellationToken) ??
                throw new PostNotFoundException();

            if (post.UserId != _identityService.UserId)
                throw new UnauthorizedOperationException();

            var blobNames = await _blobService.UploadAsync(Post.MediaContainerName, request.Media, cancellationToken);

            var media = CreatePostHelpers.GenerateMedia(types, blobNames);

            try
            {
                post.AddMedia(media, request.Offset);
                await _postRepository.UpdateAsync(post, cancellationToken);

                var @event = _mapper.Map<Post, PostMediaCreatedEvent>(post);
                await _publishEndpoint.Publish(@event, cancellationToken);
            }
            catch (Exception)
            {
                await _blobService.DeleteAsync(Post.MediaContainerName, blobNames, cancellationToken);
                throw;
            }
        }
    }
}
