using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using QueryService.Application.UseCases.CommentUseCases.UpdateComent;
using Shared.Events.Comment;

namespace QueryService.Workers.Consumers.CommentDomain.SetCommentContentModerationResult
{
    internal class SetCommentContentModerationResult_QueryService(IMediator mediator, IMapper mapper) : IConsumer<CommentContentModerationResultSetEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<CommentContentModerationResultSetEvent> context) =>
            _mediator.Send(
                _mapper.Map<CommentContentModerationResultSetEvent, UpdateCommentRequest>(context.Message),
                context.CancellationToken
            );
    }
}
