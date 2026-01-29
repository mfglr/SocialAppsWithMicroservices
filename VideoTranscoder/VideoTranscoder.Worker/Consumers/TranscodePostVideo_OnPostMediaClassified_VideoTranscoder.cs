using MassTransit;
using MediatR;
using Shared.Events;
using Shared.Events.PostMediaService;
using VideoTranscoder.Application.UseCases.TranscodeVideo;

namespace VideoTranscoder.Worker.Consumers
{
    internal class TranscodePostVideo_OnPostMediaClassified_VideoTranscoder(ISender sender, IPublishEndpoint publishEndpoint) : IConsumer<PostMediaClassifiedEvent>
    {
        public async Task Consume(ConsumeContext<PostMediaClassifiedEvent> context)
        {
            if (context.Message.Type != MediaType.Video) return;

            var response = await sender.Send(
                new TranscodeVideoRequest(context.Message.ContainerName, context.Message.BlobName),
                context.CancellationToken
            );
            await publishEndpoint
                .Publish(
                    new PostVideoTranscodedEvent(
                        context.Message.Id,
                        context.Message.BlobName,
                        response.BlobName
                    ),
                    context.CancellationToken
                );
        }
    }
}
