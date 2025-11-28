using Shared.Objects;

namespace Shared.Events.Media
{
    public record MediaPreprocessingCompletedEvent(
        Guid Id,
        Guid OwnerId,
        string BlobName,
        string? TranscodedBlobName,
        Metadata MetaData,
        ModerationResult ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails
    );
}
