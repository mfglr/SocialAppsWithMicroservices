using Shared.Objects;

namespace UserService.Application.UseCases.GetUserById
{
    public record GetUserByIdResponse(
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
