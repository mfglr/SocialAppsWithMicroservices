namespace Shared.Events.UserService
{
    public record UserMediaMetadataExtractedEvent(Guid Id, string BlobName, Metadata Metadata);
}
