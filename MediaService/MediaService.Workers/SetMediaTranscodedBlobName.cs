using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using MediaService.Application.UseCases;
using MediaService.Application.UseCases.SetMediaTranscodedBlobName;
using Shared.Events.Media;

namespace MediaService.Workers
{
    internal class SetMediaTranscodedBlobName(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint) : IConsumer<VideoTranscodedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<VideoTranscodedEvent> context)
        {
            var client = _mediator.CreateRequestClient<SetMediaTranscodedBlobNameRequest>();
            var response = await client.GetResponse<MediaResponse>(
                new SetMediaTranscodedBlobNameRequest(
                    context.Message.Id,
                    context.Message.BlobName
                ),
                context.CancellationToken
            );

            if (response.Message.IsPreprocessingCompleted)
                await _publishEndpoint
                    .Publish(
                        _mapper.Map<MediaResponse, MediaPreprocessingCompletedEvent>(response.Message),
                        context.CancellationToken
                    );
        }
    }
}
