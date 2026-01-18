using MediatR;
using Microsoft.AspNetCore.Http;

namespace ThumbnailGenerator.Application.UseCases.GenerateFileThumbnail
{
    public record GenerateFileThumbnailRequest(IFormFile File, double Resolution, bool IsSquare) : IRequest<byte[]>;
}
