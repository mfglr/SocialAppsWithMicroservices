namespace Shared.Events.PostMediaService
{
    public record PostMediaMetadataExtractedEvent(Guid Id, string ContainerName, string BlobName, MediaType Type, Metadata Metadata);
}
