using Shared.Objects;

namespace CommentService.Application.UseCases.RestorePostComments
{
    public record RestorePostCommentsResponse_Content(
       string Value,
       ModerationResult ModerationResult
   );
    public record RestorePostCommentsResponse_Comment(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        bool IsDeleted,
        int Version,
        Guid UserId,
        Guid PostId,
        Guid? ParentId,
        Guid? RepliedId,
        RestorePostCommentsResponse_Content Content
    );
    public record RestorePostCommentsResponse(IEnumerable<RestorePostCommentsResponse_Comment> Comments);
}
