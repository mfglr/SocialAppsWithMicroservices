using Shared.Objects;

namespace CommentService.Application.UseCases.SetCommentContentModerationResult
{
    public record SetCommentContentModerationResultRequest(Guid Id, ModerationResult ModerationResult);
}
