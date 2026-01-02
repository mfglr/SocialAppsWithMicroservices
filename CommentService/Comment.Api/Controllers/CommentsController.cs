using AutoMapper;
using CommentService.Application.UseCases.CreateComment;
using CommentService.Application.UseCases.DeleteComment;
using CommentService.Application.UseCases.RestoreComment;
using CommentService.Application.UseCases.UpdateCommentContent;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Events.Comment;

namespace Comment.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CommentsController(IMediator mediator, IPublishEndpoint publishEndpoint, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IMapper _mapper = mapper;

        [Authorize("Password",Roles = "user")]
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

        [Authorize("Password", Roles = "user")]
        [HttpPut]
        public async Task UpdateContent(UpdateCommentContentRequest request, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<UpdateCommentContentRequest>();
            var response = await client.GetResponse<UpdateCommentContentResponse>(request, cancellationToken);
            await _publishEndpoint.Publish(
                _mapper.Map<UpdateCommentContentResponse, CommentContentUpdatedEvent>(response.Message),
                cancellationToken
            );
        }

        [Authorize("Password", Roles = "user, admin")]
        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<DeleteCommentRequest>();
            var response = await client.GetResponse<DeleteCommentResponse>(new DeleteCommentRequest(id), cancellationToken);
            await _publishEndpoint.Publish(
                _mapper.Map<DeleteCommentResponse, CommentDeletedEvent>(response.Message),
                cancellationToken
            );
        }

        [Authorize("Password", Roles = "admin")]
        [HttpPut]
        public async Task Restore(RestoreCommentRequest request, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<RestoreCommentRequest>();
            var response = await client.GetResponse<RestoreCommentResponse>(request, cancellationToken);
            await _publishEndpoint.Publish(
                _mapper.Map<RestoreCommentResponse, CommentRestoredEvent>(response.Message),
                cancellationToken
            );
        }
    }
}
