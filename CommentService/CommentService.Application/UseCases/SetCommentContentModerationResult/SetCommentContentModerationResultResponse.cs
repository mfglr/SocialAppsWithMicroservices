using Shared.Objects;

namespace CommentService.Application.UseCases.SetCommentContentModerationResult
{
    public record SetCommentContentModerationResultResponse_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record SetCommentContentModerationResultResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        bool IsDeleted,
        Guid UserId,
        Guid PostId,
        int Version,
        SetCommentContentModerationResultResponse_Content Content
    );
}
