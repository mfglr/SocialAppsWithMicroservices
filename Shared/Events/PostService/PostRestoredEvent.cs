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
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        string? TranscodedBlobName
    );
    public record PostRestoredEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        int Version,
        bool IsDeleted,
        PostRestoredEvent_Content? Content,
        IEnumerable<PostRestoredEvent_Media> Media
    );
}
