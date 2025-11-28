using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using MediaService.Application.UseCases.SetMediaThumbnail;
using Shared.Events.Media;

namespace MediaService.Workers
{
    internal class SetMediaThumbnail(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint) : IConsumer<MediaThumbnailGeneratedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<MediaThumbnailGeneratedEvent> context)
        {
            var client = _mediator.CreateRequestClient<SetMediaThumbnailRequest>();
            var response = await client.GetResponse<SetMediaThumbnailResponse>(
                new SetMediaThumbnailRequest(
                    context.Message.Id,
                    context.Message.Thumbnail
                ),
                context.CancellationToken
            );

            if (response.Message.IsPreprocessingCompleted)
                await _publishEndpoint.Publish(
                    _mapper.Map<SetMediaThumbnailResponse, MediaPreprocessingCompletedEvent>(response.Message),
                    context.CancellationToken
                );
        }
    }
}
