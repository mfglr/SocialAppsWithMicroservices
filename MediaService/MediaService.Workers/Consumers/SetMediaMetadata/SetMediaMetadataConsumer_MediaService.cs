using MassTransit;
using MediaService.Application.UseCases.SetMediaMetadata;
using MediatR;
using Shared.Events.MediaService;

namespace MediaService.Workers.Consumers.SetMediaMetadata
{
    internal class SetMediaMetadataConsumer_MediaService(ISender sender) : IConsumer<MediaMetadataExtractedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaMetadataExtractedEvent> context) =>
            _sender.Send(new SetMediaMetadataRequest(context.Message.Id, context.Message.Metadata), context.CancellationToken);
    }
}
