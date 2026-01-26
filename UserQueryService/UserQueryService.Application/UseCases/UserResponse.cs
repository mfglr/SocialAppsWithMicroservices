using Shared.Objects;

namespace UserQueryService.Application.UseCases
{
    public record UserResponse(
        string Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        string? Name,
        string UserName,
        string Gender,
        IEnumerable<Media> Media
    );
}
