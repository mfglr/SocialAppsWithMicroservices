using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.CommentUseCases.UpdateComent;
using Shared.Events.Comment;

namespace QueryService.Workers.Consumers.CommentDomain.DeleteComment
{
    internal class DeleteCommentConsumer_QueryService(ISender _sender, IMapper mapper) : IConsumer<CommentDeletedEvent>
    {
        private readonly ISender _sender = _sender;
        private readonly IMapper _mapper = mapper;
        public Task Consume(ConsumeContext<CommentDeletedEvent> context) =>
            _sender.Send(
                _mapper.Map<CommentDeletedEvent, UpdateCommentRequest>(context.Message),
                context.CancellationToken
            );
    }
}
