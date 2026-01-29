using MassTransit;
using MediatR;
using PostMedia.Application.UseCases.SetPostMediaMetadata;
using Shared.Events.PostMediaService;

namespace PostMedia.Worker.Consumers
{
    internal class SetPostMediaMetadata_OnPostMediaMetadataExtracted_PostMediaService(ISender sender) : IConsumer<PostMediaMetadataExtractedEvent>
    {
        public Task Consume(ConsumeContext<PostMediaMetadataExtractedEvent> context) =>
            sender
                .Send(
                    new SetPostMediaMetadataRequest(
                        context.Message.Id,
                        context.Message.BlobName,
                        context.Message.Metadata
                    ),
                    context.CancellationToken
                );
    }
}
