using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using QueryService.Application.UseCases.PostUseCases.GetPostById;

namespace QueryService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id:guid}")]
        public async Task<GetPostByIdResponse> GetById(Guid id, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<GetPostByIdRequest>();
            var response = await client.GetResponse<GetPostByIdResponse>(new GetPostByIdRequest(id), cancellationToken);
            return response.Message;
        }
    }
}
