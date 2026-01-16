using QueryService.Application.UseCases.PostUseCases;
using QueryService.Domain.PostDomain;

namespace QueryService.Infrastructure.QueryRepositories
{
    internal static class PostResponseQueryMapper
    {
        public static IQueryable<PostResponse> ToPostResponse(this IQueryable<Post> posts, SqlContext context) =>
            posts.
                Join(
                    context.Users,
                    post => post.UserId,
                    user => user.Id,
                    (post, user) => new PostResponse(
                        post.Id,
                        post.CreatedAt,
                        post.UpdatedAt,
                        post.UserId,
                        post.Content != null 
                            ?   new PostResponse_Content(
                                    post.Content.Value,
                                    post.Content.ModerationResult
                                )
                            :   null,
                        post.Media,
                        user.Username,
                        user.Name,
                        user.Media.FirstOrDefault()
                    )
                );
    }
}
