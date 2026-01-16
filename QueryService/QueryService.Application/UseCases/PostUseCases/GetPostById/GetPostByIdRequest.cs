using MediatR;

namespace QueryService.Application.UseCases.PostUseCases.GetPostById
{
    public record GetPostByIdRequest(Guid Id) : IRequest<PostResponse>;
}
