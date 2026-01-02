using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.UseCases.CreatePost;
using PostService.Application.UseCases.CreatePostMedia;
using PostService.Application.UseCases.DeletePost;
using PostService.Application.UseCases.DeletePostMedia;
using PostService.Application.UseCases.RestorePost;
using PostService.Application.UseCases.UpdatePostContent;
using Shared.Events.PostService;

namespace PostService.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class PostsController(IMediator mediator, IPublishEndpoint publishEndpoint, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        [Authorize("user")]
        [HttpPost]
        public async Task<Guid> Create([FromForm] CreatePostRequest request, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<CreatePostRequest>();
            var response = await client.GetResponse<CreatePostResponse>(request, cancellationToken);

            await _publishEndpoint.Publish(
                _mapper.Map<CreatePostResponse,PostCreatedEvent>(response.Message),
                cancellationToken
            );
            return response.Message.Id;
        }

        [Authorize("user")]
        [HttpPut]
        public async Task CreateMedia([FromForm] CreatePostMediaRequest request, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<CreatePostMediaRequest>();
            var response = await client.GetResponse<CreatePostMediaResponse>(request, cancellationToken);

            await _publishEndpoint.Publish(
                _mapper.Map<CreatePostMediaResponse, PostMediaCreatedEvent>(response.Message),
                cancellationToken
            );
        }

        [Authorize("user")]
        [HttpPut]
        public async Task DeleteMedia(DeletePostMediaRequest request, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<DeletePostMediaRequest>();
            var response = await client.GetResponse<DeletePostMediaResponse>(request, cancellationToken);
            
            await _publishEndpoint.Publish(
                _mapper.Map<DeletePostMediaResponse, PostMediaDeletedEvent>(response.Message),
                cancellationToken
            );
        }

        [Authorize("user")]
        [HttpPut]
        public async Task UpdateContent(UpdatePostContentRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            await _publishEndpoint.Publish(
                new PostContentUpdatedEvent(request.Id, request.Content),
                cancellationToken
            );
        }

        [Authorize("adminOrUser")]
        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<DeletePostRequest>();
            var response = await client.GetResponse<DeletePostResponse>(
                new DeletePostRequest(id),
                cancellationToken
            );
            await _publishEndpoint.Publish(
                _mapper.Map<DeletePostResponse, PostDeletedEvent>(response.Message),
                cancellationToken
            );
        }

        [Authorize("admin")]
        [HttpPut]
        public async Task Restore(RestorePostRequest request, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<RestorePostRequest>();
            var response = await client.GetResponse<RestorePostResponse>(request,cancellationToken);
            await _publishEndpoint.Publish(
                _mapper.Map<RestorePostResponse,PostRestoredEvent>(response.Message),
                cancellationToken
            );
        }
    }
}
