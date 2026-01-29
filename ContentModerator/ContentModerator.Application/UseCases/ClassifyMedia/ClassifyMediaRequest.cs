using MediatR;
using Shared.Events;

namespace ContentModerator.Application.UseCases.ClassifyMedia
{
    public record ClassifyMediaRequest(string ContainerName, string BlobName, MediaType Type) : IRequest<ModerationResult>;
}
