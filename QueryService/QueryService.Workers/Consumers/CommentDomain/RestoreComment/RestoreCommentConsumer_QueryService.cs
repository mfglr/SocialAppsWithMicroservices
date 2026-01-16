using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.CommentUseCases.UpdateComent;
using Shared.Events.Comment;

namespace QueryService.Workers.Consumers.CommentDomain.RestoreComment
{
    internal class RestoreCommentConsumer_QueryService(ISender sender, IMapper mapper) : IConsumer<CommentRestoredEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<CommentRestoredEvent> context) =>
            _sender.Send(
                _mapper.Map<CommentRestoredEvent, UpdateCommentRequest>(context.Message),
                context.CancellationToken
            );
    }
}
