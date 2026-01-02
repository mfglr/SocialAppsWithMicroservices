using AutoMapper;
using MassTransit;
using PostService.Domain;

namespace PostService.Application.UseCases.CreatePost
{
    public class CreatePostConsumer(IPostRepository postRepository, IBlobService blobService, IMapper mapper, IIdentityService identityService) : IConsumer<CreatePostRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IBlobService _blobService = blobService;
        private readonly IMapper _mapper = mapper;
        private readonly IIdentityService _identityService = identityService;

        public async Task Consume(ConsumeContext<CreatePostRequest> context)
        {
            var userId = _identityService.UserId;
            var types = CreatePostHelpers.GetMediaTypes(context.Message.Media);
            var content = new Content(context.Message.Content);
            var blobNames = await _blobService.UploadAsync(Post.MediaContainerName, context.Message.Media, context.CancellationToken);
            var media = CreatePostHelpers.GenerateMedia(types, blobNames);

            try
            {
                var post = new Post(userId, content, media);
                post.Create();
                await _postRepository.CreateAsync(post, context.CancellationToken);

                await context.RespondAsync(_mapper.Map<Post, CreatePostResponse>(post));
            }
            catch (Exception)
            {
                await _blobService.DeleteAsync(Post.MediaContainerName, blobNames, context.CancellationToken);
                throw;
            }
        }
    }
}
