using AutoMapper;
using MassTransit;
using PostService.Domain;
using PostService.Domain.Exceptions;

namespace PostService.Application.UseCases.DeletePost
{
    internal class DeletePostConsumer(IPostRepository postRepository, IMapper mapper) : IConsumer<DeletePostRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IMapper _mapper = mapper;
        public async Task Consume(ConsumeContext<DeletePostRequest> context)
        {
            var post =
                await _postRepository.GetByIdAsync(context.Message.Id, context.CancellationToken) ??
                throw new PostNotFoundException();

            post.Delete();
            await _postRepository.UpdateAsync(post, context.CancellationToken);
            await context.RespondAsync(_mapper.Map<Post, DeletePostResponse>(post));
        }
    }
}
