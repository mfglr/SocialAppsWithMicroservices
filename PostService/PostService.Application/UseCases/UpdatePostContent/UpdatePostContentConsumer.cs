using MassTransit;
using PostService.Domain;

namespace PostService.Application.UseCases.UpdatePostContent
{
    internal class UpdatePostContentConsumer(IPostRepository postRepository) : IConsumer<UpdatePostContentRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task Consume(ConsumeContext<UpdatePostContentRequest> context)
        {
            var content = new Content(context.Message.Content);
            var post = 
                await _postRepository.GetByIdAsync(context.Message.Id, context.CancellationToken) ??
                throw new Exception("Post not found exception");
            post.UpdateContent(content);
            await _postRepository.UpdateAsync(post, context.CancellationToken);
        }
    }
}
