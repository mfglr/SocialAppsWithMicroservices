using Shared.Objects;

namespace QueryService.Application.UseCases.PostUseCases
{

    public record PostResponse_Content(
        string Value,
        ModerationResult ModerationResult
    );

    public record PostResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        PostResponse_Content? Content,
        IEnumerable<Media> Media,
        string UserName,
        string? Name,
        Media? ProfilePhoto
    );
}
