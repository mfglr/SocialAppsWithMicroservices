using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.UseCases.CreateUser;
using UserService.Application.UseCases.SendEmailVerificationMail;

namespace UserService.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UsersController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public Task Create(CreateUserRequest request, CancellationToken cancellationToken)
            => _sender.Send(request, cancellationToken);

        [HttpGet]
        [Authorize("user")]
        public Task SendEmailVerificationMail(CancellationToken cancellationToken) =>
            _sender.Send(new SendEmailVeificationMailRequest(), cancellationToken);
    }
}
