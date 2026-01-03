using AutoMapper;
using MassTransit;
using PostService.Application.Exceptions;
using PostService.Domain;
using PostService.Domain.Exceptions;

namespace PostService.Application.UseCases.DeletePost
{
    public class DeletePostConsumer(IPostRepository postRepository, IMapper mapper, IIdentityService identityService) : IConsumer<DeletePostRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IIdentityService _identityService = identityService;

        public async Task Consume(ConsumeContext<DeletePostRequest> context)
        {
            var post =
                await _postRepository.GetByIdAsync(context.Message.Id, context.CancellationToken) ??
                throw new PostNotFoundException();

            if (!_identityService.IsAdminOrOwner(post.UserId))
                throw new UnauthorizedOperationException();

            post.Delete();
            await _postRepository.UpdateAsync(post, context.CancellationToken);
            await context.RespondAsync(_mapper.Map<Post, DeletePostResponse>(post));
        }
    }
}
