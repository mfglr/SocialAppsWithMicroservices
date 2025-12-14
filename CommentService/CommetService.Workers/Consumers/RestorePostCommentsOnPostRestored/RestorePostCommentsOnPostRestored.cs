using AutoMapper;
using CommentService.Application.UseCases.RestorePostComments;
using MassTransit;
using MassTransit.Mediator;
using Shared.Events.Comment;
using Shared.Events.PostService;

namespace CommetService.Workers.Consumers.RestorePostCommentsOnPostRestored
{
    internal class RestorePostCommentsOnPostRestored(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint) : IConsumer<PostRestoredEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<PostRestoredEvent> context)
        {
            var client = _mediator.CreateRequestClient<RestorePostCommentsRequest>();
            var request = new RestorePostCommentsRequest(context.Message.Id);
            var response = await client.GetResponse<RestorePostCommentsResponse>(request,context.CancellationToken);
            var events = 
                _mapper.Map<IEnumerable<RestorePostCommentsResponse_Comment>, IEnumerable<CommentRestoredEvent>>(
                    response.Message.Comments
                );
            await _publishEndpoint.PublishBatch(events, context.CancellationToken);
        }
    }
}
