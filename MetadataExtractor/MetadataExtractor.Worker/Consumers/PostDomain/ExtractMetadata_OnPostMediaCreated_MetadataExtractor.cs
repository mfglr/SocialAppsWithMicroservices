using MassTransit;
using MediatR;
using MetadataExtractor.Application.UseCases.ExtractMetadata;
using Shared.Events.PostMediaService;

namespace MetadataExtractor.Worker.Consumers.PostDomain
{
    internal class ExtractMetadata_OnPostMediaCreated_MetadataExtractor(ISender sender, IPublishEndpoint publishEndpoint) : IConsumer<PostMediaCreatedEvent>
    {
        public async Task Consume(ConsumeContext<PostMediaCreatedEvent> context)
        {
            var metadata = await sender
                .Send(
                    new ExtractMetadataRequest(context.Message.ContainerName, context.Message.BlobName),
                    context.CancellationToken
                );

            await publishEndpoint
                .Publish(
                    new PostMediaMetadataExtractedEvent(
                        context.Message.Id,
                        context.Message.ContainerName,
                        context.Message.BlobName,
                        context.Message.Type,
                        metadata
                    ),
                    context.CancellationToken
                );
        }
    }
}
