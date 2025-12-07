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
            var next = _mapper.Map<UpdatePostRequest, Post>(context.Message);
            if (!next.IsValidVersion) return;

            var prev = await _postRepository.GetByIdAsync(context.Message.Id, context.CancellationToken);
            if (prev != null)
                prev.Set(next);
            else
                await _postRepository.CreateAsync(next, context.CancellationToken);
            await _unitOfWork.CommitAsync(context.CancellationToken);
        }
    }
}
