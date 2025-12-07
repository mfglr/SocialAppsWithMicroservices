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
        IReadOnlyList<Thumbnail> Thumbnails
    );
    public record SetPostMediaResponse(
        Guid Id,
        int Version,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        SetPostMediaResponse_Content? Content,
        IReadOnlyList<SetPostMediaResponse_Media> Media
    );
}
