using Shared.Objects;

namespace QueryService.Application.UseCases.CommentUseCases
{
    public record CommentResponse_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record CommentResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid PostId,
        Guid UserId,
        CommentResponse_Content Content
    );
}
