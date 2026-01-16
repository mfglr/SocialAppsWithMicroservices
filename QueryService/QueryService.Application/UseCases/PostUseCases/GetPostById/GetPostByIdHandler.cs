using MediatR;
using QueryService.Application.QueryRepositories;

namespace QueryService.Application.UseCases.PostUseCases.GetPostById
{
    public class GetPostByIdHandler(IPostQueryRepository postRepository) : IRequestHandler<GetPostByIdRequest, PostResponse>
    {
        private readonly IPostQueryRepository _postRepository = postRepository;

        public async Task<PostResponse> Handle(GetPostByIdRequest request, CancellationToken cancellationToken)
        {
            var post =
               await _postRepository.GetByIdAsync(request.Id, cancellationToken) ??
               throw new PostNotFoundException();
            return post;
        }
    }
}
