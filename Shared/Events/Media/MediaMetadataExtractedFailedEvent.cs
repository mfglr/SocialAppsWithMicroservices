using Shared.Objects;

namespace Shared.Events.Media
{
    public record MediaMetadataExtractedFailedEvent(Guid Id, Metadata Metadata);
}
