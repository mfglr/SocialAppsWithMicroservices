using Shared.Objects;

namespace PostService.Application.UseCases.RestorePost
{
    public record RestorePostResponse_Content(string Value, ModerationResult? ModerationResult);
    public record RestorePostResponse_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata Metadata,
        ModerationResult ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails,
        bool IsDeleted
    );
    public record RestorePostResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int Version,
        bool IsDeleted,
        RestorePostResponse_Content? Content,
        IReadOnlyList<RestorePostResponse_Media> Media
    );
}
