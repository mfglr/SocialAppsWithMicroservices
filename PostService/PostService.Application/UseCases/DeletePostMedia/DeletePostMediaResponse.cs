using Shared.Objects;

namespace PostService.Application.UseCases.DeletePostMedia
{
    public record DeletePostMediaResponse_Content(string Value, ModerationResult? ModerationResult);
    public record DeletePostMediaResponse_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata Metadata,
        ModerationResult ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails,
        bool IsDeleted
    );
    public record DeletePostMediaResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int Version,
        bool IsDeleted,
        DeletePostMediaResponse_Content? Content,
        IReadOnlyList<DeletePostMediaResponse_Media> Media
    );
}
