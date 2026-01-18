using MassTransit;
using MediaService.Application.UseCases.SetMediaModerationResult;
using MediatR;
using Shared.Events.MediaService;

namespace MediaService.Workers.Consumers.SetMediaModerationResul
{
    internal class SetMediaModerationResult(ISender sender) : IConsumer<MediaClassfiedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaClassfiedEvent> context) =>
            _sender.Send(new SetMediaModerationResultRequest(context.Message.Id, context.Message.ModerationResult), context.CancellationToken);
    }
}
