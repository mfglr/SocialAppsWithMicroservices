using ContentModerator.Application.UseCases.ClassifyText;
using MassTransit;
using MediatR;
using Shared.Events.PostService;

namespace ContentModerator.Worker.Consumers.PostDomain
{
    internal class ClassifyPostContent_OnPostContentUpdated_ContentModerator(ISender sender, IPublishEndpoint publishEndpoint) : IConsumer<PostContentUpdatedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        
        public async Task Consume(ConsumeContext<PostContentUpdatedEvent> context)
        {
            if (context.Message.Content == null) return;
            
            var result = await _sender.Send(new ClassifyTextRequest(context.Message.Content.Value), context.CancellationToken);
            
            await _publishEndpoint.Publish(
                new PostContentClassifiedEvent(context.Message.Id, result),
                context.CancellationToken
            );
        }
    }
}
