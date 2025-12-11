using AutoMapper;
using MassTransit;
using QueryService.Domain.CommentDomain;
namespace QueryService.Application.UseCases.CommentUseCases.UpdateComent
{
    internal class UpdateCommentConsumer(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IMapper mapper) : IConsumer<UpdateCommentRequest>
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<UpdateCommentRequest> context)
        {

            var prev = await _commentRepository.GetByIdAsync(context.Message.Id, context.CancellationToken);

            if (prev != null && context.Message.Version <= prev.Version)
                return;

            if (prev == null && context.Message.IsDeleted)
                return;

            if (prev != null && context.Message.IsDeleted)
            {
                _commentRepository.Delete(prev);
                await _unitOfWork.CommitAsync(context.CancellationToken);
                return;
            }

            if(prev != null)
            {
                var content = _mapper.Map<UpdateCommentRequest_Content, CommentContent>(context.Message.Content);
                prev.Set(context.Message.UpdatedAt, context.Message.Version, content);
                await _unitOfWork.CommitAsync(context.CancellationToken);
                return;
            }

            var next = _mapper.Map<UpdateCommentRequest, Comment>(context.Message);
            await _commentRepository.CreateAsync(next,context.CancellationToken);
            await _unitOfWork.CommitAsync(context.CancellationToken);
        }
    }
}
