using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.UseCases.CreatePost;
using PostService.Application.UseCases.CreatePostMedia;
using PostService.Application.UseCases.DeletePostMedia;
using PostService.Application.UseCases.UpdatePostContent;
using Shared.Events.PostService;

namespace PostService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController(IMediator mediator, IPublishEndpoint publishEndpoint, IMapper mapper) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        [HttpPost]
        public async Task<Guid> Create([FromForm] string content, [FromForm] IFormFileCollection media, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<CreatePostRequest>();
            var response = await client.GetResponse<CreatePostResponse>(new(content, media), cancellationToken);

            await _publishEndpoint.Publish(
                _mapper.Map<CreatePostResponse,PostCreatedEvent>(response.Message),
                cancellationToken
            );
            return response.Message.Id;
        }

        [HttpPut]
        public async Task CreateMedia([FromForm] Guid id, [FromForm] IFormFileCollection media, [FromForm] int index, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<CreatePostMediaRequest>();
            var response = await client.GetResponse<CreatePostMediaResponse>(new CreatePostMediaRequest(id, media, index), cancellationToken);

            await _publishEndpoint.Publish(
                _mapper.Map<CreatePostMediaResponse, PostMediaCreatedEvent>(response.Message),
                cancellationToken
            );
        }

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

        [HttpPut]
        public async Task UpdateContent(UpdatePostContentRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            await _publishEndpoint.Publish(
                new PostContentUpdatedEvent(request.Id, request.Content),
                cancellationToken
            );
        }

        
    }
}
