namespace Shared.Events.PostMediaService
{
    public record PostMediaPreproccessingCompletedEvent_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        string? TranscodedBlobName
    );
    public record PostMediaPreproccessingCompletedEvent(
        Guid Id,
        IEnumerable<PostMediaPreproccessingCompletedEvent_Media> Media
    );
}
