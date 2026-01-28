namespace UserService.Application.UseCases.GetUserById
{
    public record GetUserByIdRequest(Guid Id) : MediatR.IRequest<GetUserByIdResponse>;
}
