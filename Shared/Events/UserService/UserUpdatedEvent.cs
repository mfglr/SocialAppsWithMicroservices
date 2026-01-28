using Shared.Objects;

namespace Shared.Events.UserService
{
    public record UserUpdatedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int Version,
        bool IsDeleted,
        string? Name,
        string Username,
        string Gender,
        IEnumerable<Media> Media
    );
}
