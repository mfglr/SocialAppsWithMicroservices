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
        IReadOnlyList<Thumbnail> Thumbnails
    )
    {
        public IEnumerable<string> BlobNames => TranscodedBlobName != null
            ? [BlobName, TranscodedBlobName, .. Thumbnails.Select(x => x.BlobName)]
            : [BlobName, .. Thumbnails.Select(x => x.BlobName)];
    }
    public record PostMediaDeletedEvent(
        Guid Id,
        int Version,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        PostMediaDeletedEvent_Content? Content,
        IReadOnlyList<PostMediaDeletedEvent_Media> Media,
        PostMediaDeletedEvent_Media DeletedMedia
    );
}
