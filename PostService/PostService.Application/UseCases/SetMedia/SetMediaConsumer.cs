using MassTransit;
using PostService.Domain;

namespace PostService.Application.UseCases.SetMedia
{
    internal class SetMediaConsumer(IPostRepository postRepository) : IConsumer<SetMediaRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task Consume(ConsumeContext<SetMediaRequest> context)
        {
            var post = (await _postRepository.GetByIdAsync(context.Message.Id, context.CancellationToken))!;
            post.SetMedia(
                context.Message.BlobName,
                context.Message.TranscodedBlobName,
                context.Message.Metadata,
                context.Message.ModerationResult,
                context.Message.Thumbnailes
            );
            await _postRepository.UpdateAsync(post, context.CancellationToken);
        }
    }
}
