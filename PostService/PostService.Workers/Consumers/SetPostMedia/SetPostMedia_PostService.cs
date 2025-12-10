using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using PostService.Application.UseCases.SetPostMedia;
using Shared.Events.Media;
using Shared.Events.PostService;

namespace PostService.Workers.Consumers.SetPostMedia
{
    internal class SetPostMedia_PostService(IMapper mapper, IPublishEndpoint publishEndpoint, IMediator mediator) : IConsumer<MediaPreprocessingCompletedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<MediaPreprocessingCompletedEvent> context)
        {
            var client = _mediator.CreateRequestClient<SetPostMediaRequest>();
            var request = _mapper.Map<MediaPreprocessingCompletedEvent, SetPostMediaRequest>(context.Message);
            var response = await client.GetResponse<SetPostMediaResponse>(
                request,
                context.CancellationToken
            );
            await _publishEndpoint
                .Publish(
                    _mapper.Map<SetPostMediaResponse, PostMediaSetEvent>(response.Message),
                    context.CancellationToken
                );
        }
    }
}
