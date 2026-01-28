using MediatR;
using Shared.Objects;

namespace ContentModerator.Application.UseCases.ClassifyImage
{
    public record ClassifyImageRequest(string ContainerName, string BlobName) : IRequest<ModerationResult>;
}
