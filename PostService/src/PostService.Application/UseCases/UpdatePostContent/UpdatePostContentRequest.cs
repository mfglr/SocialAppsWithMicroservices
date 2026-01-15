using MediatR;

namespace PostService.Application.UseCases.UpdatePostContent
{
    public record UpdatePostContentRequest(Guid Id, string Content) : IRequest;
}
