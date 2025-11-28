using ContentModerator.Application.UseCases.ClassifyText;
using MassTransit;
using MassTransit.Mediator;
using Shared.Events.PostService;
using Shared.Objects;

namespace ContentModerator.Worker
{
    internal class ClassifyPostContent(IMediator mediator, IPublishEndpoint publishEndpoint) : IConsumer<PostCreatedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<PostCreatedEvent> context)
        {
            if (context.Message.Content == null) return;

            var client = _mediator.CreateRequestClient<ClassifyTextRequest>();
            var response = await client.GetResponse<ModerationResult>(new ClassifyTextRequest(context.Message.Content));
            await _publishEndpoint.Publish(
                new PostContentClassifiedEvent(context.Message.Id, response.Message),
                context.CancellationToken
            );
        }
    }
}
