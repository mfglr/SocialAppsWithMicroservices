using AutoMapper;
using CommentService.Application.Exceptions;
using CommentService.Domain;
using MassTransit;

namespace CommentService.Application.UseCases.SetCommentContentModerationResult
{
    internal class SetCommentContentModerationResultConsumer(ICommentRepository commentRepository, IMapper mapper) : IConsumer<SetCommentContentModerationResultRequest>
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<SetCommentContentModerationResultRequest> context)
        {
            var comment = 
                await _commentRepository.GetByIdAsync(context.Message.Id, context.CancellationToken) ??
                throw new CommentNotFoundException();
            comment.SetModerationResult(context.Message.ModerationResult);
            await _commentRepository.UpdateAsync(comment, context.CancellationToken);
            await context.RespondAsync(_mapper.Map<Comment, SetCommentContentModerationResultResponse>(comment));
        }
    }
}
