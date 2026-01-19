using MassTransit;
using MediaService.Application.UseCases.DeleteMedia;
using MediatR;
using Shared.Events.MediaService;

namespace MediaService.Workers.Consumers.DeleteMedia
{
    internal class DeleteMedia_OnMediaPreprocessingCompleted_MediaService(ISender sender) : IConsumer<MediaPreprocessingCompletedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaPreprocessingCompletedEvent> context) =>
            _sender.Send(new DeleteMediaRequest(context.Message.Id), context.CancellationToken);
    }
}
