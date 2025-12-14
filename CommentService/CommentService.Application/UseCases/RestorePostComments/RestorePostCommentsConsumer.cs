using AutoMapper;
using CommentService.Domain;
using MassTransit;

namespace CommentService.Application.UseCases.RestorePostComments
{
    internal class RestorePostCommentsConsumer(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IMapper mapper) : IConsumer<RestorePostCommentsRequest>
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<RestorePostCommentsRequest> context)
        {
            var comments = await _commentRepository.GetByPostIdAsync(context.Message.PostId, context.CancellationToken);
            foreach (var comment in comments)
                comment.Restore();
            await _unitOfWork.CommitAsync(context.CancellationToken);
            var response = new RestorePostCommentsResponse(
                _mapper.Map<IEnumerable<Comment>, IEnumerable<RestorePostCommentsResponse_Comment>>(comments)
            );
            await context.RespondAsync(response);
        }
    }
}
