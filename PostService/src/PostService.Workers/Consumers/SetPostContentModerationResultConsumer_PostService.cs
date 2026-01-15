using MassTransit;
using MediatR;
using PostService.Application.UseCases.SetPostContentModerationResult;
using Shared.Events.PostService;

namespace PostService.Workers.Consumers
{
    internal class SetPostContentModerationResultConsumer_PostService(ISender sender) : IConsumer<PostContentClassifiedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<PostContentClassifiedEvent> context) =>
            _sender
                .Send(
                    new SetPostContentModerationResultRequest(
                        context.Message.Id,
                        context.Message.ModerationResult
                    ),
                    context.CancellationToken
                );
    }
}
