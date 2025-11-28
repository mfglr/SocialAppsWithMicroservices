using Shared.Objects;

namespace MediaService.Application.UseCases.SetMediaTranscodedBlobName
{
    public record SetMediaTranscodedBlobNameResponse(
        Guid Id,
        int Version,
        Guid OwnerId,
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata MetaData,
        ModerationResult ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails
    )
    {
        public bool IsPreprocessingCompleted =>
            (Type == MediaType.Image && Version == 4) ||
            (Type == MediaType.Video && Version == 5);
    }
}
