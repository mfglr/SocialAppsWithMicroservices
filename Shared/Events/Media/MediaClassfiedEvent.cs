using Shared.Objects;

namespace Shared.Events.Media
{
    public record MediaClassfiedEvent(Guid Id, ModerationResult ModerationResult);
}
