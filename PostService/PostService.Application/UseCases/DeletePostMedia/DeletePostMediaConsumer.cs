using AutoMapper;
using MassTransit;
using PostService.Application.Exceptions;
using PostService.Domain;

namespace PostService.Application.UseCases.DeletePostMedia
{
    internal class DeletePostMediaConsumer(IPostRepository postRepository, IMapper mapper) : IConsumer<DeletePostMediaRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<DeletePostMediaRequest> context)
        {
            var post =
                await _postRepository.GetByIdAsync(context.Message.Id, context.CancellationToken) ??
                throw new PostNotFoundException();

            if (post.IsDeleted)
                throw new PostNotFoundException();

            post.DeleMedia(context.Message.BlobName);
            await _postRepository.UpdateAsync(post, context.CancellationToken);
            await context.RespondAsync(_mapper.Map<Post, DeletePostMediaResponse>(post));
        }
    }
}
