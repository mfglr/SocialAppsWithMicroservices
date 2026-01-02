using Shared.Objects;

namespace Shared.Events.PostService
{
    public record PostRestoredEvent_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record PostRestoredEvent_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails,
        bool IsDeleted
    );
    public record PostRestoredEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        int Version,
        bool IsDeleted,
        PostRestoredEvent_Content? Content,
        IReadOnlyList<PostRestoredEvent_Media> Media
    );
}
