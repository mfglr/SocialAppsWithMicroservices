using Shared.Objects;

namespace Shared.Events.UserService
{
    public record UserMediaCreatedEvent(
        Guid Id,
        Media Media
    );
}
