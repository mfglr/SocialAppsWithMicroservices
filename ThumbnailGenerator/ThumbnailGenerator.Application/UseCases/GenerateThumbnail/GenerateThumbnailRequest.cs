using MediatR;
using Shared.Objects;

namespace ThumbnailGenerator.Application.UseCases.GenerateThumbnail
{
    public record GenerateThumbnailRequest(string ContainerName, string BlobName, double Resulation, bool IsSquare) : IRequest<Thumbnail>;
}
