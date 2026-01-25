using MediatR;
using Shared.Objects;

namespace UserQueryService.Application.UseCases.UpsertUser
{
    public record UpsertUserRequest(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int Version,
        bool IsDeleted,
        string? Name,
        string Username,
        string Gender,
        IEnumerable<Media> Media
    ) : IRequest;
}
