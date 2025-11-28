using Shared.Objects;

namespace Shared.Events.Media
{
    public record MediaMeatadataExtractedEvent(Guid Id, Metadata Metadata);
}
