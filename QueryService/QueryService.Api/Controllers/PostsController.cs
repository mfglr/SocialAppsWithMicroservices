using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueryService.Application.UseCases.PostUseCases;
using QueryService.Application.UseCases.PostUseCases.GetPostById;

namespace QueryService.Api.Controllers
{
    [Authorize($"ClientCredential")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;
        [HttpGet("{id:guid}")]
        public Task<PostResponse> GetById(Guid id, CancellationToken cancellationToken) =>
            _sender.Send(new GetPostByIdRequest(id), cancellationToken);
    }
}
