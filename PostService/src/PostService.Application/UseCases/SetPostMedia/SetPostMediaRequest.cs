using MediatR;
using Shared.Objects;

namespace PostService.Application.UseCases.SetPostMedia
{
    public record SetPostMediaRequest(
        Guid OwnerId,
        string BlobName,
        string? TranscodedBlobName,
        Metadata Metadata,
        ModerationResult? ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails
    ) : IRequest;
}
