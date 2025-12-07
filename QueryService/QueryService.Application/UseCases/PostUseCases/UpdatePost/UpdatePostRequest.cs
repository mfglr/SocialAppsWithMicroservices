using Shared.Objects;

namespace QueryService.Application.UseCases.PostUseCases.UpdatePost
{
    public record UpdatePostRequest_Content(string Value, ModerationResult ModerationResult);
    public record UpdatePostRequest_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata Metadata,
        ModerationResult ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails
    );
    public record UpdatePostRequest(
        Guid Id,
        int Version,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        UpdatePostRequest_Content? Content,
        IReadOnlyList<UpdatePostRequest_Media> Media
    );
}
