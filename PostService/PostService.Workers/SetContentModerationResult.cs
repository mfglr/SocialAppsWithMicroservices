using MassTransit;
using MassTransit.Mediator;
using PostService.Application.UseCases.SetContentModerationResult;
using Shared.Events.PostService;

namespace PostService.Workers
{
    internal class SetContentModerationResult(IMediator mediator) : IConsumer<PostContentClassifiedEvent>
    {
        private readonly IMediator _mediator = mediator;

        public Task Consume(ConsumeContext<PostContentClassifiedEvent> context) =>
            _mediator.Send(
                new SetContentModerationResultRequest(
                    context.Message.Id,
                    context.Message.ModerationResult
                ),
                context.CancellationToken
            );
    }
}
