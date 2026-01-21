using Microsoft.EntityFrameworkCore;
using QueryService.Application.Pagination;
using QueryService.Application.QueryRepositories;
using QueryService.Application.UseCases.PostUseCases;
using QueryService.Domain.PostDomain;
using Shared.Objects;
using System.Text.Json;

namespace QueryService.Infrastructure.QueryRepositories
{
    internal class PostQueryRepository(SqlContext context) : IPostQueryRepository
    {
        private readonly SqlContext _context = context;

        public async Task<PostResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            (
                await _context.Posts
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .ToInternalPostResponse(_context)
                    .FirstOrDefaultAsync(cancellationToken)
            )?.ToPostResponse();

        public async Task<List<PostResponse>> GetPostsByUserId(Guid userId, Page<DateTime> page, CancellationToken cancellationToken) =>
            (
                await _context.Posts
                    .AsNoTracking()
                    .Where(x => x.UserId == userId)
                    .ToPage(page)
                    .ToInternalPostResponse(_context)
                    .ToListAsync(cancellationToken)
            ).ToPostResponse();
            
    }

    internal static class PostResponseMapper
    {
        public static PostResponse ToPostResponse(this InternalPostResponse post) =>
            new (
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
                JsonSerializer.Deserialize<IEnumerable<Media>>(post.Media) ?? [],
                post.UserName,
                post.Name,
                post.ProfilePhoto
            );

        public static List<PostResponse> ToPostResponse(this IEnumerable<InternalPostResponse> posts) =>
            [.. posts.Select(x => x.ToPostResponse())];
    }

    public record InternalPostResponse_Content(
        string Value,
        ModerationResult ModerationResult
    );

    internal record InternalPostResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        InternalPostResponse_Content? Content,
        string Media,
        string UserName,
        string? Name,
        Media? ProfilePhoto
    );

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

    internal static class InternalPostResponseQueryMapper
    {
        public static IQueryable<InternalPostResponse> ToInternalPostResponse(this IQueryable<Post> posts, SqlContext context) =>
            posts.
                Join(
                    context.Users,
                    post => post.UserId,
                    user => user.Id,
                    (post, user) => new InternalPostResponse(
                        post.Id,
                        post.CreatedAt,
                        post.UpdatedAt,
                        post.UserId,
                        post.Content != null
                            ? new InternalPostResponse_Content(
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
