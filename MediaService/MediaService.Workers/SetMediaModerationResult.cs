using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using MediaService.Application.UseCases.SetMediaModerationResult;
using Shared.Events.Media;

namespace MediaService.Workers
{
    internal class SetMediaModerationResult(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint) : IConsumer<MediaClassfiedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<MediaClassfiedEvent> context)
        {
            var client = _mediator.CreateRequestClient<SetMediaModerationResultRequest>();
            var response = await client.GetResponse<SetMediaModerationResultResponse>(
               new SetMediaModerationResultRequest(context.Message.Id, context.Message.ModerationResult)
            );

            if (response.Message.IsPreprocessingCompleted)
                await _publishEndpoint.Publish(
                    _mapper.Map<SetMediaModerationResultResponse, MediaPreprocessingCompletedEvent>(response.Message),
                    context.CancellationToken
                );

        }
    }
}
