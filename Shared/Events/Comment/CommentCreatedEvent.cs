using Shared.Objects;

namespace Shared.Events.Comment
{
    public record CommentCreatedEvent_Content(string Value, ModerationResult ModerationResult);
    public record CommentCreatedEvent(Guid Id, Guid UserId, Guid PostId, CommentCreatedEvent_Content Content);
}
