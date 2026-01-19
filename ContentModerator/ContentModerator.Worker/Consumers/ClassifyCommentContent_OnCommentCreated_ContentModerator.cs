using ContentModerator.Application.UseCases.ClassifyText;
using MassTransit;
using MediatR;
using Shared.Events.Comment;

namespace ContentModerator.Worker.Consumers
{
    internal class ClassifyCommentContent_OnCommentCreated_ContentModerator(ISender sender, IPublishEndpoint publishEndpoint) : IConsumer<CommentCreatedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<CommentCreatedEvent> context)
        {
            var result = await _sender.Send(new ClassifyTextRequest(context.Message.Content.Value), context.CancellationToken);

            await _publishEndpoint.Publish(
                new CommentContentClassifiedEvent(context.Message.Id, result),
                context.CancellationToken
            );
        }
    }
}
