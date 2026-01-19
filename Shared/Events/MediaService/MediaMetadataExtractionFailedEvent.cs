using Shared.Objects;

namespace Shared.Events.MediaService
{
    public record MediaMetadataExtractionFailedEvent(Guid Id, string ContainerName, string BlobName, MediaType Type, Metadata Metadata);
}
