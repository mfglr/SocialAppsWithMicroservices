namespace CommentService.Application.UseCases.CreateComment
{
    public record CreateCommentRequest(Guid UserId, Guid PostId, string Content);
}
