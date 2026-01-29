using MediatR;

namespace PostService.Application.UseCases.GetPostById
{
    public record GetPostByIdRequest(Guid Id) : IRequest<GetPostByIdResponse>;
}
