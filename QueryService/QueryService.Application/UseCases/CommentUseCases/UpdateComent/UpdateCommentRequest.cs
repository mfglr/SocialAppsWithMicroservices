using Shared.Objects;

namespace QueryService.Application.UseCases.CommentUseCases.UpdateComent
{

    public record UpdateCommentRequest_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record UpdateCommentRequest(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        bool IsDeleted,
        Guid UserId,
        Guid PostId,
        int Version,
        UpdateCommentRequest_Content Content
    );
}
