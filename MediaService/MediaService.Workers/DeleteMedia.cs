using MassTransit;
using MassTransit.Mediator;
using MediaService.Application.UseCases.DeleteMedia;
using Shared.Events.Media;

namespace MediaService.Workers
{
    internal class DeleteMedia(IMediator mediator) : IConsumer<MediaPreprocessingCompletedEvent>
    {
        private readonly IMediator _mediator = mediator;

        public Task Consume(ConsumeContext<MediaPreprocessingCompletedEvent> context) =>
            _mediator.Send(
                new DeleteMediaRequest(context.Message.Id),
                context.CancellationToken
            );
    }
}
