using MediatR;

namespace PostService.Application.UseCases.DeletePost
{
    public record DeletePostRequest(Guid Id) : IRequest;
}
