using MassTransit;
using MediatR;
using Shared.Events.MediaService;
using ThumbnailGenerator.Application.UseCases.GenerateThumbnail;

namespace ThumbnailGenerator.Workers.Consumers
{
    internal class Generate360SquareThumbnail_OnMediaMetadaExtractedSuccess_ThumbnailGenerator(ISender sender) : IConsumer<MediaMetadataExtractionSuccessEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaMetadataExtractionSuccessEvent> context) =>
            _sender
                .Send(
                    new GenerateThumbnailRequest(
                        context.Message.Id,
                        context.Message.ContainerName,
                        context.Message.BlobName,
                        360,
                        true
                    ),
                    context.CancellationToken
                );
    }
}
