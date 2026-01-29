using ContentModerator.Application.UseCases.ClassifyMedia;
using MassTransit;
using MediatR;
using Shared.Events.PostMediaService;

namespace ContentModerator.Worker.Consumers.PostDomain
{
    internal class ClassifyPostMedia_OnPostMediaMetadataExtracted_ContentModerator(ISender sender, IPublishEndpoint publishEndpoint) : IConsumer<PostMediaMetadataExtractedEvent>
    {
        public async Task Consume(ConsumeContext<PostMediaMetadataExtractedEvent> context)
        {
            if (context.Message.Metadata.Duration > 300) return;

            var result = await sender.Send(
                new ClassifyMediaRequest(
                    context.Message.ContainerName,
                    context.Message.BlobName,
                    context.Message.Type
                ),
                context.CancellationToken
            );

            await publishEndpoint
                .Publish(
                    new PostMediaClassifiedEvent(
                        context.Message.Id,
                        context.Message.ContainerName,
                        context.Message.BlobName,
                        context.Message.Type,
                        result
                    ),
                    context.CancellationToken
                );
        }
    }
}
