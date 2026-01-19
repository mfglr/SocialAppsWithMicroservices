using MassTransit;
using MediatR;
using MetadataExtractor.Application.UseCases.ExtractMediaMetadata;
using Shared.Events.MediaService;

namespace MetadataExtractor.Worker.Consumers
{
    internal class ExtractMediaMetadata_OnMediaCreated_MetadataExtractor(ISender sender) : IConsumer<MediaCreatedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaCreatedEvent> context) =>
            _sender
                .Send(
                    new ExtractMediaMetadataRequest(
                        context.Message.Id,
                        context.Message.ContainerName,
                        context.Message.BlobName,
                        context.Message.Type
                    ),
                    context.CancellationToken
                );
    }
}
