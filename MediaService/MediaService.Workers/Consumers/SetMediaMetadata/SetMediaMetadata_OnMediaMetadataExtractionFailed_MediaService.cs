using MassTransit;
using MediaService.Application.UseCases.SetMediaMetadata;
using MediatR;
using Shared.Events.MediaService;

namespace MediaService.Workers.Consumers.SetMediaMetadata
{
    internal class SetMediaMetadata_OnMediaMetadataExtractionFailed_MediaService(ISender sender) : IConsumer<MediaMetadataExtractionFailedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaMetadataExtractionFailedEvent> context) =>
            _sender.Send(new SetMediaMetadataRequest(context.Message.Id, context.Message.Metadata), context.CancellationToken);
    }
}
