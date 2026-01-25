using MediatR;

namespace UserQueryService.Application.UseCases.GetById
{
    public record GetByIdRequest(string Id) : IRequest<UserResponse>;
}
