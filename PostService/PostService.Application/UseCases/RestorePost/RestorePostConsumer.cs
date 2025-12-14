using AutoMapper;
using MassTransit;
using PostService.Domain;
using PostService.Domain.Exceptions;

namespace PostService.Application.UseCases.RestorePost
{
    internal class RestorePostConsumer(IPostRepository postRepository, IMapper mapper) : IConsumer<RestorePostRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<RestorePostRequest> context)
        {
            var post =
                await _postRepository.GetByIdAsync(context.Message.Id, context.CancellationToken) ??
                throw new PostNotFoundException();
            post.Restore();
            await _postRepository.UpdateAsync(post, context.CancellationToken);
            await context.RespondAsync(_mapper.Map<Post, RestorePostResponse>(post));
        }
    }
}
