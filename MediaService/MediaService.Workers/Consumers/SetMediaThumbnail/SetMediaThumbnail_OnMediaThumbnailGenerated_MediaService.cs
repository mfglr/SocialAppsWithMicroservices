using MassTransit;
using MediaService.Application.UseCases.SetMediaThumbnail;
using MediatR;
using Shared.Events.MediaService;

namespace MediaService.Workers.Consumers.SetMediaThumbnail
{
    internal class SetMediaThumbnail_OnMediaThumbnailGenerated_MediaService(ISender sender) : IConsumer<MediaThumbnailGeneratedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaThumbnailGeneratedEvent> context) =>
            _sender.Send(new SetMediaThumbnailRequest(context.Message.Id, context.Message.Thumbnail), context.CancellationToken);
    }
}
