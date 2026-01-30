namespace PostQueryService.Domain
{
    public record Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IReadOnlyList<Thumbnail> Thumbnails,
        string? TranscodedBlobName
    );
}
