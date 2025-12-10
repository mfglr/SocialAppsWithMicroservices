using AutoMapper;
using MassTransit;
using PostService.Application.Exceptions;
using PostService.Domain;

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

            if (post.IsDeleted)
                throw new PostNotFoundException();

            post.Delete();
            await _postRepository.UpdateAsync(post, context.CancellationToken);
            await context.RespondAsync(_mapper.Map<Post, DeletePostResponse>(post));
        }
    }
}
