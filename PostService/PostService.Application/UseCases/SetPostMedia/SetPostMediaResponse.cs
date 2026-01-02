using Shared.Objects;

namespace PostService.Application.UseCases.SetPostMedia
{
    public record SetPostMediaResponse_Content(string Value, ModerationResult? ModerationResult);
    public record SetPostMediaResponse_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails,
        bool IsDeleted
    );
    public record SetPostMediaResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        bool IsDeleted,
        int Version,
        SetPostMediaResponse_Content? Content,
        IReadOnlyList<SetPostMediaResponse_Media> Media
    );
}
