using Shared.Objects;

namespace Shared.Events.Media
{
    public record MediaMetadataExtractedSuccessEvent(Guid Id, string ContainerName, string BlobName, MediaType Type, Metadata Metadata);
}
