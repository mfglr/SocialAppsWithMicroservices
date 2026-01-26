using MediatR;

namespace UserQueryService.Application.UseCases.GetByUserName
{
    public record GetByUserNameRequest(string UserName) : IRequest<UserResponse>;
}
