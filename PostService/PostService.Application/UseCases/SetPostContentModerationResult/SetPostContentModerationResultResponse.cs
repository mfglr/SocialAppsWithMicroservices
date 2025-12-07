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
        IReadOnlyList<Thumbnail> Thumbnails
    );
    public record SetPostContentModerationResultResponse(
        Guid Id,
        int Version,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        SetPostContentModerationResultResponse_Content Content,
        IReadOnlyList<SetPostContentModerationResultResponse_Media> Media
    );
}
