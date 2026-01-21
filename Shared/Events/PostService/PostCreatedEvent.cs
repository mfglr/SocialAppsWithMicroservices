using Shared.Objects;

namespace Shared.Events.PostService
{
    public record PostCreatedEvent_Content(string Value);
    public record PostCreatedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        int Version,
        bool IsDeleted,
        PostDeletedEvent_Content? Content,
        IReadOnlyList<Media> Media
    );
}
