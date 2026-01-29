using AutoMapper;
using MediatR;
using PostService.Domain;
using PostService.Domain.Exceptions;

namespace PostService.Application.UseCases.GetPostById
{
    internal class GetPostByIdHandler(IPostRepository repository, IMapper mapper) : IRequestHandler<GetPostByIdRequest, GetPostByIdResponse>
    {
        public async Task<GetPostByIdResponse> Handle(GetPostByIdRequest request, CancellationToken cancellationToken)
        {
            var post = await repository.GetByIdAsync(request.Id,cancellationToken) ?? throw new PostNotFoundException();
            return mapper.Map<Post, GetPostByIdResponse>(post);
        }
    }
}
