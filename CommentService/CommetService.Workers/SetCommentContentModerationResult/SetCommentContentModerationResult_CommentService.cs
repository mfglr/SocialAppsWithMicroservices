using AutoMapper;
using CommentService.Application.UseCases.SetCommentContentModerationResult;
using MassTransit;
using MassTransit.Mediator;
using Shared.Events.Comment;

namespace CommetService.Workers.SetCommentContentModerationResult
{
    internal class SetCommentContentModerationResult_CommentService(IMediator mediator, IPublishEndpoint publishEndpoint, IMapper mapper) : IConsumer<CommentContentClassifiedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<CommentContentClassifiedEvent> context)
        {
            var client = _mediator.CreateRequestClient<SetCommentContentModerationResultRequest>();
            var request = _mapper.Map<CommentContentClassifiedEvent, SetCommentContentModerationResultRequest>(context.Message);
            var response = await client.GetResponse<SetCommentContentModerationResultResponse>(
                request,
                context.CancellationToken
            );

            var @event = _mapper.Map<SetCommentContentModerationResultResponse, CommentContentModerationResultSetEvent>(response.Message);
            await _publishEndpoint.Publish(@event,context.CancellationToken);
        }
    }
}
