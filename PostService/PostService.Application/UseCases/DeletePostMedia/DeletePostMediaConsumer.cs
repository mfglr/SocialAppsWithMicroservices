using AutoMapper;
using MassTransit;
using PostService.Application.Exceptions;
using PostService.Domain;
using PostService.Domain.Exceptions;

namespace PostService.Application.UseCases.DeletePostMedia
{
    public class DeletePostMediaConsumer(IPostRepository postRepository, IMapper mapper, IIdentityService identityService) : IConsumer<DeletePostMediaRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IIdentityService _identityService = identityService;

        public async Task Consume(ConsumeContext<DeletePostMediaRequest> context)
        {
            var post =
                await _postRepository.GetByIdAsync(context.Message.Id, context.CancellationToken) ??
                throw new PostNotFoundException();

            if(_identityService.UserId != post.UserId)
                throw new UnauthorizedOperationException();

            post.DeleMedia(context.Message.BlobName);
            await _postRepository.UpdateAsync(post, context.CancellationToken);
            await context.RespondAsync(_mapper.Map<Post, DeletePostMediaResponse>(post));
        }
    }
}
