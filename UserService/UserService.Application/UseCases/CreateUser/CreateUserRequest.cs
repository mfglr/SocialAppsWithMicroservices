using MediatR;

namespace UserService.Application.UseCases.CreateUser
{
    public record CreateUserRequest(string Email, string Password) : MediatR.IRequest;
}
