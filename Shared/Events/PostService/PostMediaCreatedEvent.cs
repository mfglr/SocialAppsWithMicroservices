using Shared.Objects;

namespace Shared.Events.PostService
{
    public record PostMediaCreatedEvent_Media(string ContainerName, string BlobName, MediaType Type);
    public record PostMediaCreatedEvent(
        Guid Id,
        IReadOnlyList<PostMediaCreatedEvent_Media> Media
    );
}
