using MassTransit;
using MassTransit.Mediator;
using PostService.Application.UseCases.SetMedia;
using Shared.Events.Media;

namespace PostService.Workers
{
    internal class SetMedia(IMediator mediator) : IConsumer<MediaPreprocessingCompletedEvent>
    {
        private readonly IMediator _mediator = mediator;

        public Task Consume(ConsumeContext<MediaPreprocessingCompletedEvent> context) =>
            _mediator.Send(
                new SetMediaRequest(
                    context.Message.OwnerId,
                    context.Message.BlobName,
                    context.Message.TranscodedBlobName,
                    context.Message.MetaData,
                    context.Message.ModerationResult,
                    context.Message.Thumbnails
                ),
                context.CancellationToken
            );
    }
}
