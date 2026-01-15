using AutoMapper;
using MassTransit;
using MediatR;
using PostService.Domain;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.CreatePost
{
    internal class CreatePostHandler(IPostRepository postRepository, IBlobService blobService, IMapper mapper, IIdentityService identityService, IPublishEndpoint publishEndpoint) : IRequestHandler<CreatePostRequest, CreatePostResponse>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IBlobService _blobService = blobService;
        private readonly IMapper _mapper = mapper;
        private readonly IIdentityService _identityService = identityService;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<CreatePostResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var types = CreatePostHelpers.GetMediaTypes(request.Media);
            var content = new Content(request.Content);
            var blobNames = await _blobService.UploadAsync(Post.MediaContainerName, request.Media, cancellationToken);
            var media = CreatePostHelpers.GenerateMedia(types, blobNames);
            try
            {
                var post = new Post(_identityService.UserId, content, media);
                post.Create();
                await _postRepository.CreateAsync(post, cancellationToken);

                var @event = _mapper.Map<Post, PostCreatedEvent>(post);
                await _publishEndpoint.Publish(@event,cancellationToken);

                return new(post.Id);
            }
            catch (Exception)
            {
                await _blobService.DeleteAsync(Post.MediaContainerName, blobNames, cancellationToken);
                throw;
            }
        }
    }
}
