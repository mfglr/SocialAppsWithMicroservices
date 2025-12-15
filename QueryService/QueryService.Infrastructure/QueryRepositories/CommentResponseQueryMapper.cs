using QueryService.Application.UseCases.CommentUseCases;
using QueryService.Domain.CommentDomain;

namespace QueryService.Infrastructure.QueryRepositories
{
    internal static class CommentResponseQueryMapper
    {
        public static IQueryable<CommentResponse> ToCommentResponse(this IQueryable<Comment> comments) =>
            comments
                .Select(
                    x => new CommentResponse(
                        x.Id,
                        x.CreatedAt,
                        x.UpdatedAt,
                        x.PostId,
                        x.UserId,
                        new CommentResponse_Content(
                            x.Content.Value,
                            x.Content.ModerationResult
                        )
                    )
                );
    }
}
