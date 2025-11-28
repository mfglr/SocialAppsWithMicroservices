using Shared.Objects;

namespace MediaService.Application.UseCases
{
    public record MediaResponse(
        Guid Id,
        int Version,
        Guid OwnerId,
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata MetaData,
        ModerationResult? ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails
    )
    {
        public bool IsPreprocessingCompleted =>
            (Type == MediaType.Image && Version == 4) ||
            (Type == MediaType.Video && Version == 5 && MetaData.Duration <= 180) ||
            (Type == MediaType.Video && Version == 4 && MetaData.Duration > 180);
    }
}
