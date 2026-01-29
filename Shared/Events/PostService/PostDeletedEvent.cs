namespace Shared.Events.PostService
{
    public record PostDeletedEvent_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record PostDeletedEvent_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        string? TranscodedBlobName
    );
    public record PostDeletedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        int Version,
        bool IsDeleted,
        PostDeletedEvent_Content? Content,
        IEnumerable<PostDeletedEvent_Media> Media
    );
}
