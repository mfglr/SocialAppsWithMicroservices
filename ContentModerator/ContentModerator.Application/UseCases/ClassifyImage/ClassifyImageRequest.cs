using MediatR;
using Shared.Events;

namespace ContentModerator.Application.UseCases.ClassifyImage
{
    public record ClassifyImageRequest(string ContainerName, string BlobName) : IRequest<ModerationResult>;
}
