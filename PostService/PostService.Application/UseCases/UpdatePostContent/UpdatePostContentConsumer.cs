using MassTransit;
using PostService.Domain;
using PostService.Domain.Exceptions;

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
                throw new PostNotFoundException();
            post.UpdateContent(content);
            await _postRepository.UpdateAsync(post, context.CancellationToken);
        }
    }
}
