using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.UseCases.CreatePost;
using PostService.Application.UseCases.DeletePost;
using PostService.Application.UseCases.GetPostById;
using PostService.Application.UseCases.RestorePost;
using PostService.Application.UseCases.UpdatePostContent;

namespace PostService.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class PostsController(ISender sender) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public Task<GetPostByIdResponse> GetById(Guid id, CancellationToken cancellationToken) =>
            sender.Send(new GetPostByIdRequest(id), cancellationToken);

        [Authorize("user")]
        [HttpPost]
        public Task<CreatePostResponse> Create([FromForm] CreatePostRequest request, CancellationToken cancellationToken) =>
            sender.Send(request, cancellationToken);

        [Authorize("adminOrUser")]
        [HttpDelete("{id:guid}")]
        public Task Delete(Guid id, CancellationToken cancellationToken) =>
            sender.Send(new DeletePostRequest(id), cancellationToken);

        [Authorize("admin")]
        [HttpPut]
        public Task Restore(RestorePostRequest request, CancellationToken cancellationToken) =>
            sender.Send(request, cancellationToken);

        [Authorize("user")]
        [HttpPut]
        public Task UpdateContent(UpdatePostContentRequest request, CancellationToken cancellationToken) =>
            sender.Send(request, cancellationToken);
    }
}
