using Shared.Objects;

namespace PostService.Application.UseCases.DeletePost
{
    public record DeletePostResponse_Content(string Value, ModerationResult ModerationResult);
    public record DeletePostResponse_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails,
        bool IsDeleted
    );
    public record DeletePostResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        bool IsDeleted,
        int Version,
        DeletePostResponse_Content Content,
        IReadOnlyList<DeletePostResponse_Media> Media
    );
}
