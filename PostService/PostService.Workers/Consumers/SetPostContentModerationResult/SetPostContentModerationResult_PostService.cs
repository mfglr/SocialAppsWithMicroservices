using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using PostService.Application.UseCases.SetPostContentModerationResult;
using Shared.Events.PostService;

namespace PostService.Workers.Consumers.SetPostContentModerationResult
{
    internal class SetPostContentModerationResult_PostService(IMapper mapper, IPublishEndpoint publishEndpoint, IMediator mediator) : IConsumer<PostContentClassifiedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<PostContentClassifiedEvent> context)
        {
            var client = _mediator.CreateRequestClient<SetPostContentModerationResultRequest>();

            var response = await client.GetResponse<SetPostContentModerationResultResponse>(
                new(context.Message.Id, context.Message.ModerationResult),
                context.CancellationToken
            );

            await _publishEndpoint.Publish(
                _mapper.Map<SetPostContentModerationResultResponse, PostContentModerationResultSetEvent>(response.Message),
                context.CancellationToken
            );
        }
    }
}
