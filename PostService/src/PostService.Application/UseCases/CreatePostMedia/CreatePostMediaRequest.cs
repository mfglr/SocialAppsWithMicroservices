using MediatR;
using Microsoft.AspNetCore.Http;

namespace PostService.Application.UseCases.CreatePostMedia
{
    public record CreatePostMediaRequest(Guid Id, IFormFileCollection Media, string? Offset) : IRequest;
}
