using AutoMapper;
using MassTransit;
using QueryService.Domain.PostDomain;

namespace QueryService.Application.UseCases.PostUseCases.GetPostById
{
    public class GetPostByIdConsumer(IPostRepository postRepository, IMapper mapper) : IConsumer<GetPostByIdRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<GetPostByIdRequest> context)
        {
            var post =
               await _postRepository.GetByIdAsync(context.Message.Id, context.CancellationToken) ??
               throw new Exception("Post not found exception");
            var response = _mapper.Map<Post,GetPostByIdResponse>(post);
            await context.RespondAsync(response);
        }
    }
}
