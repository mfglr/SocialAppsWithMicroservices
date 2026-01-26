using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserQueryService.Application.UseCases;
using UserQueryService.Application.UseCases.GetById;
using UserQueryService.Application.UseCases.GetByUserName;

namespace UserQueryService.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UsersController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpGet("{id}")]
        public Task<UserResponse> GetById(string id, CancellationToken cancellationToken) =>
            _sender.Send(new GetByIdRequest(id), cancellationToken);

        [HttpGet("{userName}")]
        public Task<UserResponse> GetByUserName(string userName, CancellationToken cancellationToken) =>
            _sender.Send(new GetByUserNameRequest(userName), cancellationToken);
    }
}
