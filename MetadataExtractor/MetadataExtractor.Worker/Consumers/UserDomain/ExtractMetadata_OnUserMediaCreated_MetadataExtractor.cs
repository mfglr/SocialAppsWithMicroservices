using MassTransit;
using MediatR;
using MetadataExtractor.Application.UseCases.ExtractMetadata;
using Shared.Events.UserService;

namespace MetadataExtractor.Worker.Consumers.UserDomain
{
    internal class ExtractMetadata_OnUserMediaCreated_MetadataExtractor(ISender sender, IPublishEndpoint publishEndpoint) : IConsumer<UserMediaCreatedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<UserMediaCreatedEvent> context)
        {
            var metadata = await _sender.Send(new ExtractMetadataRequest(context.Message.Media.ContainerName, context.Message.Media.BlobName));
            await _publishEndpoint.Publish(
                new UserMediaMetadataExtractedEvent(context.Message.Id, context.Message.Media.BlobName, metadata),
                context.CancellationToken
            );
        }
    }
}
