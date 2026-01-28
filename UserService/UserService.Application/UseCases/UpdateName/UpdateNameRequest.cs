using MediatR;

namespace UserService.Application.UseCases.UpdateName
{
    public record UpdateNameRequest(string Name) : MediatR.IRequest;
}
