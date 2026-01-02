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
        IReadOnlyList<Thumbnail> Thumbnails,
        bool IsDeleted
    );
    public record PostMediaSetEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        int Version,
        bool IsDeleted,
        PostMediaSetEvent_Content? Content,
        IReadOnlyList<PostMediaSetEvent_Media> Media
    );
}
