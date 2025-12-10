using MassTransit;
using PostService.Application.Exceptions;
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
                throw new PostNotFoundException();

            if(post.IsDeleted)
                throw new PostNotFoundException();

            post.UpdateContent(content);
            await _postRepository.UpdateAsync(post, context.CancellationToken);
        }
    }
}
