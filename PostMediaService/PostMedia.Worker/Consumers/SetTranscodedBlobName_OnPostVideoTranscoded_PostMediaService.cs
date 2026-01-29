using MassTransit;
using MediatR;
using PostMedia.Application.UseCases.SetPostVideoTranscodedBlobName;
using Shared.Events.PostMediaService;

namespace PostMedia.Worker.Consumers
{
    internal class SetTranscodedBlobName_OnPostVideoTranscoded_PostMediaService(ISender sender) : IConsumer<PostVideoTranscodedEvent>
    {
        public Task Consume(ConsumeContext<PostVideoTranscodedEvent> context) =>
            sender
                .Send(
                    new SetPostVideoTranscodedBlobNameRequest(
                        context.Message.Id,
                        context.Message.BlobName,
                        context.Message.TranscodedBlobName
                    ),
                    context.CancellationToken
                );
    }
}
