using MassTransit;
using MassTransit.Mediator;
using MetadataExtractor.Application.UseCases.ExtractMediaMetadata;
using Shared.Events.Media;
using Shared.Objects;

namespace MetadataExtractor.Worker
{
    internal class ExtractMediaMetadata(IMediator mediator, IPublishEndpoint publishEndpoint) : IConsumer<MediaCreatedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<MediaCreatedEvent> context)
        {
            var client = _mediator.CreateRequestClient<ExtractMediaMetadataRequest>();
            var response = await client.GetResponse<Metadata>(new ExtractMediaMetadataRequest(
                context.Message.ContainerName,
                context.Message.BlobName
            ));

            await _publishEndpoint.Publish(
                new MediaMeatadataExtractedEvent(context.Message.Id,response.Message),
                context.CancellationToken
            );
        }
    }
}
