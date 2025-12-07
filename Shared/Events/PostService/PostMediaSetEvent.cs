using Shared.Objects;

namespace Shared.Events.PostService
{
    public record PostMediaSetEvent_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record PostMediaSetEvent_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails
    );
    public record PostMediaSetEvent(
        Guid Id,
        int Version,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        PostMediaSetEvent_Content? Content,
        IReadOnlyList<PostMediaSetEvent_Media> Media
    );
}
