using Shared.Objects;

namespace Shared.Events.PostService
{
    public record PostMediaDeletedEvent_Content(string Value, ModerationResult ModerationResult);
    public record PostMediaDeletedEvent_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata Metadata,
        ModerationResult ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails,
        bool IsDeleted
    );
    public record PostMediaDeletedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int Version,
        bool IsDeleted,
        PostMediaDeletedEvent_Content? Content,
        IReadOnlyList<PostMediaDeletedEvent_Media> Media
    );
}
