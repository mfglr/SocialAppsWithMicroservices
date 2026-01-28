using MediatR;

namespace UserService.Application.UseCases.SendEmailVerificationMail
{
    public record SendEmailVeificationMailRequest : MediatR.IRequest;
}
