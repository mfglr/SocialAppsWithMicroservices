using Shared.Objects;

namespace PostService.Application.UseCases.SetPostContentModerationResult
{
    public record SetPostContentModerationResultResponse_Content(string Value, ModerationResult ModerationResult);
    public record SetPostContentModerationResultResponse_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails,
        bool IsDeleted
    );
    public record SetPostContentModerationResultResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        bool IsDeleted,
        int Version,
        SetPostContentModerationResultResponse_Content Content,
        IReadOnlyList<SetPostContentModerationResultResponse_Media> Media
    );
}
