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
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        string? TranscodedBlobName
    );
    public record PostContentModerationResultSetEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        int Version,
        bool IsDeleted,
        PostContentModerationResultSetEvent_Content? Content,
        IEnumerable<PostContentModerationResultSetEvent_Media> Media
    );
}
