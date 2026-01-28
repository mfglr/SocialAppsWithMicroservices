using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.UseCases.CreateMedia;
using UserService.Application.UseCases.CreateUser;
using UserService.Application.UseCases.GetUserById;
using UserService.Application.UseCases.SendEmailVerificationMail;
using UserService.Application.UseCases.UpdateName;

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

        [HttpPost]
        [Authorize("user")]
        public Task CreateMedia([FromForm]CreateMediaRequest request ,CancellationToken cancellationToken) =>
            _sender.Send(request, cancellationToken);

        [HttpGet]
        [Authorize("user")]
        public Task SendEmailVerificationMail(CancellationToken cancellationToken) =>
            _sender.Send(new SendEmailVeificationMailRequest(), cancellationToken);

        [HttpPut]
        [Authorize("user")]
        public Task UpdateName(UpdateNameRequest request, CancellationToken cancellationToken) =>
            _sender.Send(request, cancellationToken);

        [HttpGet("{id:guid}")]
        public Task<GetUserByIdResponse> GetWriteModelById(Guid id,CancellationToken cancellationToken) =>
            _sender.Send(new GetUserByIdRequest(id), cancellationToken);
    }
}
