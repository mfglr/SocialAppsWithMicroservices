using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.CommentUseCases.UpdateComent;
using Shared.Events.Comment;

namespace QueryService.Workers.Consumers.CommentDomain.SetCommentContentModerationResult
{
    internal class SetCommentContentModerationResultConsumer_QueryService(ISender sender, IMapper mapper) : IConsumer<CommentContentModerationResultSetEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<CommentContentModerationResultSetEvent> context) =>
            _sender.Send(
                _mapper.Map<CommentContentModerationResultSetEvent, UpdateCommentRequest>(context.Message),
                context.CancellationToken
            );
    }
}
