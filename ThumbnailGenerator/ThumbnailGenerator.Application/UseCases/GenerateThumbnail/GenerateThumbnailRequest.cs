using MediatR;

namespace ThumbnailGenerator.Application.UseCases.GenerateThumbnail
{
    public record GenerateThumbnailRequest(Guid Id, string ContainerName, string BlobName, double Resulation, bool IsSquare) : IRequest;
}
