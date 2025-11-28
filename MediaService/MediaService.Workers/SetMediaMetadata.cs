using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using MediaService.Application.UseCases;
using MediaService.Application.UseCases.SetMediaMetadata;
using Shared.Events.Media;

namespace MediaService.Workers
{
    internal class SetMediaMetadata(IMediator mediator, IPublishEndpoint publishEndpoint, IMapper mapper) : IConsumer<MediaMetadataExtractedSuccessEvent>, IConsumer<MediaMetadataExtractedFailedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<MediaMetadataExtractedSuccessEvent> context)
        {
            var client = _mediator.CreateRequestClient<SetMediaMetadataRequest>();
            var response = await client.GetResponse<MediaResponse>(
                new SetMediaMetadataRequest(
                    context.Message.Id,
                    context.Message.Metadata
                ),
                context.CancellationToken
            );
            
            if (response.Message.IsPreprocessingCompleted)
                await _publishEndpoint.Publish(
                    _mapper.Map<MediaResponse, MediaPreprocessingCompletedEvent>(response.Message),
                    context.CancellationToken
                );
        }

        public async Task Consume(ConsumeContext<MediaMetadataExtractedFailedEvent> context)
        {
            var client = _mediator.CreateRequestClient<SetMediaMetadataRequest>();
            var response = await client.GetResponse<MediaResponse>(
                new SetMediaMetadataRequest(
                    context.Message.Id,
                    context.Message.Metadata
                ),
                context.CancellationToken
            );

            if (response.Message.IsPreprocessingCompleted)
                await _publishEndpoint.Publish(
                    _mapper.Map<MediaResponse, MediaPreprocessingCompletedEvent>(response.Message),
                    context.CancellationToken
                );
        }
    }
}
