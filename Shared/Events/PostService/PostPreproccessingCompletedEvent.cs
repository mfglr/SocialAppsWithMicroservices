namespace Shared.Events.PostService
{
    public record PostPreproccessingCompletedEvent_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record PostPreproccessingCompletedEvent_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        string? TranscodedBlobName
    );
    public record PostPreproccessingCompletedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        int Version,
        bool IsDeleted,
        PostPreproccessingCompletedEvent_Content? Content,
        IEnumerable<PostPreproccessingCompletedEvent_Media> Media
    );
}
