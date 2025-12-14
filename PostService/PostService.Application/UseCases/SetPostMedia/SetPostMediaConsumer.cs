using AutoMapper;
using MassTransit;
using PostService.Domain;
using PostService.Domain.Exceptions;

namespace PostService.Application.UseCases.SetPostMedia
{
    internal class SetPostMediaConsumer(IPostRepository postRepository, IMapper mapper) : IConsumer<SetPostMediaRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<SetPostMediaRequest> context)
        {
            var post =
                await _postRepository.GetByIdAsync(context.Message.OwnerId, context.CancellationToken) ??
                throw new PostNotFoundException();

            post.SetMedia(
                context.Message.BlobName,
                context.Message.TranscodedBlobName,
                context.Message.Metadata,
                context.Message.ModerationResult,
                context.Message.Thumbnails
            );
            await _postRepository.UpdateAsync(post, context.CancellationToken);
            await context.RespondAsync(_mapper.Map<Post, SetPostMediaResponse>(post));
        }
    }
}
