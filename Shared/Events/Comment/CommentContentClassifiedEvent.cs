using Shared.Objects;

namespace Shared.Events.Comment
{
    public record CommentContentClassifiedEvent(Guid Id, ModerationResult ModerationResult);
}
