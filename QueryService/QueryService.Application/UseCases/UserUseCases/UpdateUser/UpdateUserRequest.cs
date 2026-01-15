using Shared.Objects;

namespace QueryService.Application.UseCases.UserUseCases.UpdateUser
{
    public record UpdateUserRequest(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int Version,
        bool IsDeleted,
        string? Name,
        string Username,
        string Gender,
        IEnumerable<Media> Media
    );
}
