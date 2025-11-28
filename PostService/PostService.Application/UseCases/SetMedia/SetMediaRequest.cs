using Shared.Objects;

namespace PostService.Application.UseCases.SetMedia
{
    public record SetMediaRequest(
        Guid Id,
        string BlobName,
        string? TranscodedBlobName,
        Metadata Metadata,
        ModerationResult? ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnailes
    );
}
