using Shared.Objects;

namespace Shared.Events.Media
{
    public record MediaThumbnailGeneratedEvent(Guid Id, Thumbnail Thumbnail);
}
