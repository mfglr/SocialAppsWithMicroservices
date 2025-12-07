using Shared.Objects;

namespace Shared.Events.PostService
{
    public record PostContentModerationResultSetEvent_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record PostContentModerationResultSetEvent_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails
    );
    public record PostContentModerationResultSetEvent(
        Guid Id,
        int Version,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        PostContentModerationResultSetEvent_Content? Content,
        IReadOnlyList<PostContentModerationResultSetEvent_Media> Media
    );
}
