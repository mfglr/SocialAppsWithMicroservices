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
        IReadOnlyList<Thumbnail> Thumbnails,
        bool IsDeleted
    )
    {
        public bool IsValidVersion => ModerationResult != null;
    }
    public record UpdatePostRequest(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        bool IsDeleted,
        int Version,
        UpdatePostRequest_Content? Content,
        List<UpdatePostRequest_Media> Media
    )
    {
        public bool IsValidVersion => !Media.Any(x => !x.IsValidVersion);
    }
}
