using MassTransit;
using MediaService.Application.UseCases.SetMediaTranscodedBlobName;
using MediatR;
using Shared.Events.MediaService;

namespace MediaService.Workers.Consumers.SetMediaTranscodedBlobName
{
    internal class SetMediaTranscodedBlobNameConsumer_MediaService(ISender sender) : IConsumer<VideoTranscodedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<VideoTranscodedEvent> context) =>
            _sender.Send(new SetMediaTranscodedBlobNameRequest(context.Message.Id, context.Message.BlobName), context.CancellationToken);
    }
}
