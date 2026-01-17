using Microsoft.EntityFrameworkCore;
using QueryService.Application.Pagination;
using QueryService.Application.QueryRepositories;
using QueryService.Application.UseCases.PostUseCases;
using QueryService.Domain.PostDomain;

namespace QueryService.Infrastructure.QueryRepositories
{

    internal class PostQueryRepository(SqlContext context) : IPostQueryRepository
    {
        private readonly SqlContext _context = context;

        public Task<PostResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            _context.Posts
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ToPostResponse(_context)
                .FirstOrDefaultAsync(cancellationToken);

        public Task<List<PostResponse>> GetPostsByUserId(Guid userId, Page<DateTime> page, CancellationToken cancellationToken) =>
            _context.Posts
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToPage(page)
                .ToPostResponse(_context)
                .ToListAsync(cancellationToken);
    }


    internal static class ToPageExtension
    {
        public static IQueryable<Post> ToPage(this IQueryable<Post> query, Page<DateTime> page) =>
            page.IsDescending
                ? query
                    .Where(x => x.CreatedAt < page.Cursor)
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(page.RecordsPerPage)
                : query
                    .Where(x => x.CreatedAt > page.Cursor)
                    .OrderBy(x => x.CreatedAt)
                    .Take(page.RecordsPerPage);
    }

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
                            ? new PostResponse_Content(
                                    post.Content.Value,
                                    post.Content.ModerationResult
                                )
                            : null,
                        post.Media,
                        user.Username,
                        user.Name,
                        user.Media.FirstOrDefault()
                    )
                );
    }
}
