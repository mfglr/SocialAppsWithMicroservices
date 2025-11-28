using MassTransit;
using PostService.Domain;

namespace PostService.Application.UseCases.SetContentModerationResult
{
    internal class SetContentModerationResultConsumer(IPostRepository repository) : IConsumer<SetContentModerationResultRequest>
    {
        private readonly IPostRepository _repository = repository;

        public async Task Consume(ConsumeContext<SetContentModerationResultRequest> context)
        {
            var post = (await _repository.GetByIdAsync(context.Message.Id, context.CancellationToken))!;
            post.SetContentModerationResult(context.Message.ModerationResult);
            await _repository.UpdateAsync(post, context.CancellationToken);
        }
    }
}
