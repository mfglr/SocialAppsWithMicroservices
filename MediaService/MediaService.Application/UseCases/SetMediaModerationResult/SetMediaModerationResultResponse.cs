using Shared.Objects;

namespace MediaService.Application.UseCases.SetMediaModerationResult
{
    public record SetMediaModerationResultResponse(
        Guid Id,
        int Version,
        Guid OwnerId,
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata Metadata,
        ModerationResult ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails
    )
    {
        public bool IsPreprocessingCompleted =>
            (Type == MediaType.Image && Version == 4) ||
            (Type == MediaType.Video && Version == 5);
    }
}
