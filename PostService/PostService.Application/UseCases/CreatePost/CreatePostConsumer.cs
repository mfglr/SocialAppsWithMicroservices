using AutoMapper;
using MassTransit;
using PostService.Domain;

namespace PostService.Application.UseCases.CreatePost
{
    internal class CreatePostConsumer(IPostRepository postRepository, IBlobService blobService, IMapper mapper) : IConsumer<CreatePostRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IBlobService _blobService = blobService;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<CreatePostRequest> context)
        {
            var types = CreatePostConsumerHelpers.GetMediaTypes(context.Message.Media);
            var content = new Content(context.Message.Content);

            var blobNames = await _blobService.UploadAsync(Post.MediaContainerName, context.Message.Media, context.CancellationToken);
            var media = CreatePostConsumerHelpers.GenerateMedia(types, blobNames);

            try
            {
                var post = new Post(content, media);
                post.Create();
                await _postRepository.CreateAsync(post, context.CancellationToken);

                await context.RespondAsync(_mapper.Map<Post,CreatePostResponse>(post));
            }
            catch (Exception)
            {
                await _blobService.DeleteAsync(Post.MediaContainerName, blobNames, context.CancellationToken);
                throw;
            }
        }
    }
}
