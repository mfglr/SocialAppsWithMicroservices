using Shared.Objects;

namespace CommentService.Application.UseCases.CreateComment
{
    public record CreateCommentResponse_Content(string Value, ModerationResult ModerationResult);
    public record CreateCommentResponse(Guid Id, Guid UserId, Guid PostId, CreateCommentResponse_Content Content);
}
