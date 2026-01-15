using MediatR;
using Microsoft.AspNetCore.Http;

namespace PostService.Application.UseCases.CreatePost
{
    public record CreatePostRequest(string Content, IFormFileCollection Media) : IRequest<CreatePostResponse>;
}
