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
        IReadOnlyList<Thumbnail> Thumbnails
    );
    public record DeletePostMediaResponse(
        Guid Id,
        int Version,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        DeletePostMediaResponse_Content? Content,
        IReadOnlyList<DeletePostMediaResponse_Media> Media,
        DeletePostMediaResponse_Media DeletedMedia
    );
}
