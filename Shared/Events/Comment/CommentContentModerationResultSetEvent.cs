using Shared.Objects;

namespace Shared.Events.Comment
{
    public record CommentContentModerationResultSetEvent_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record CommentContentModerationResultSetEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        bool IsDeleted,
        Guid UserId,
        Guid PostId,
        int Version,
        CommentContentModerationResultSetEvent_Content Content
    );
}
