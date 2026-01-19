using MassTransit;
using MediatR;
using Shared.Events.MediaService;
using Shared.Objects;
using VideoTranscoder.Application.UseCases.TranscodeVideo;

namespace VideoTranscoder.Worker.Consumers
{
    internal class TranscodeVideo_OnMediaMetadataExtractionSuccess_VideoTranscoder(ISender sender) : IConsumer<MediaMetadataExtractionSuccessEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaMetadataExtractionSuccessEvent> context)
        {
            if (context.Message.Type != MediaType.Video) return Task.CompletedTask;

            return _sender
                .Send(
                    new TranscodeVideoRequest(
                        context.Message.Id,
                        context.Message.ContainerName,
                        context.Message.BlobName
                    ),
                    context.CancellationToken
                );
        }
    }
}
