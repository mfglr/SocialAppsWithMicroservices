using AutoMapper;
using MassTransit;
using QueryService.Domain.PostDomain;

namespace QueryService.Application.UseCases.PostUseCases.UpdatePost
{
    internal class UpdatePostConsumer(IPostRepository postRepository, IUnitOfWork unitOfWork, IMapper mapper) : IConsumer<UpdatePostRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<UpdatePostRequest> context)
        {
            if (!context.Message.IsValidVersion)
                return;
            
            var prev = await _postRepository.GetByIdAsync(context.Message.Id, context.CancellationToken);

            if (prev != null && context.Message.Version <= prev.Version)
                return;

            if (prev == null && context.Message.IsDeleted)
                return;

            if (prev != null && context.Message.IsDeleted)
            {
                _postRepository.Delete(prev);
                await _unitOfWork.CommitAsync(context.CancellationToken);
                return;
            }

            var content = context.Message.Content != null
                ? _mapper.Map<UpdatePostRequest_Content, Content>(context.Message.Content)
                : null;
            var media = _mapper.Map<IEnumerable<UpdatePostRequest_Media>, IEnumerable<Media>>(context.Message.Media);

            if (prev != null)
            {
                prev.Set(context.Message.Version, context.Message.UpdatedAt, content, media);
                await _unitOfWork.CommitAsync(context.CancellationToken);
                return;
            }

            var next = new Post(
                context.Message.Id,
                context.Message.CreatedAt,
                context.Message.UpdatedAt,
                context.Message.Version,
                content,
                media
            );
            await _postRepository.CreateAsync(next, context.CancellationToken);
            await _unitOfWork.CommitAsync(context.CancellationToken);
        }
    }
}
