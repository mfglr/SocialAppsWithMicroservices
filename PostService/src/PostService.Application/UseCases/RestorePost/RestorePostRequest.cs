using MediatR;

namespace PostService.Application.UseCases.RestorePost
{
    public record RestorePostRequest(Guid Id) : IRequest;
}
