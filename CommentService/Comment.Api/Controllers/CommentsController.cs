using AutoMapper;
using CommentService.Application.UseCases.CreateComment;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Shared.Events.Comment;

namespace Comment.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentsController(IMediator mediator, IPublishEndpoint publishEndpoint, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<Guid> Create(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<CreateCommentRequest>();
            var response = await client.GetResponse<CreateCommentResponse>(request);
            await _publishEndpoint.Publish(
                _mapper.Map<CreateCommentResponse, CommentCreatedEvent>(response.Message),
                cancellationToken
            );
            return response.Message.Id;
        }

    }
}
