namespace Shared.Events.Comment
{
    public record CommentContentUpdatedEvent_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record CommentContentUpdatedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        bool IsDeleted,
        int Version,
        Guid UserId,
        Guid PostId,
        Guid? ParentId,
        Guid? RepliedId,
        CommentContentUpdatedEvent_Content Content
    );
}
