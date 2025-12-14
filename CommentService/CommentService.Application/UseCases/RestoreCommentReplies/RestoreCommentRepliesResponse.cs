using Shared.Objects;

namespace CommentService.Application.UseCases.RestoreCommentReplies
{
    public record RestoreCommentRepliesResponse_Content(
       string Value,
       ModerationResult ModerationResult
   );
    public record RestoreCommentRepliesResponse_Comment(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        bool IsDeleted,
        int Version,
        Guid UserId,
        Guid PostId,
        Guid? ParentId,
        Guid? RepliedId,
        RestoreCommentRepliesResponse_Content Content
    );

    public record RestoreCommentRepliesResponse(IEnumerable<RestoreCommentRepliesResponse_Comment> Comments);
}
