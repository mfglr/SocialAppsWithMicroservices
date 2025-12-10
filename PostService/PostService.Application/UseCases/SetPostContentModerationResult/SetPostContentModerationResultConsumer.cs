using AutoMapper;
using MassTransit;
using PostService.Application.Exceptions;
using PostService.Domain;

namespace PostService.Application.UseCases.SetPostContentModerationResult
{
    internal class SetPostContentModerationResultConsumer(IPostRepository repository, IMapper mapper) : IConsumer<SetPostContentModerationResultRequest>
    {
        private readonly IPostRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<SetPostContentModerationResultRequest> context)
        {
            var post =
                await _repository.GetByIdAsync(context.Message.Id, context.CancellationToken) ??
                throw new PostNotFoundException();
            
            post.SetContentModerationResult(context.Message.ModerationResult);
            await _repository.UpdateAsync(post, context.CancellationToken);
            await context.RespondAsync(_mapper.Map<Post, SetPostContentModerationResultResponse>(post));
        }
    }
}
