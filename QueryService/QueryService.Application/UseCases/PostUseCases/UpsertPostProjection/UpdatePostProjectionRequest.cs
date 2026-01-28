using MediatR;
using Shared.Objects;

namespace QueryService.Application.UseCases.PostUseCases.UpsertPostProjection
{
    public record UpdatePostProjectionRequest_Content(string Value, ModerationResult ModerationResult);
    public record UpdatePostProjectionRequest(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        bool IsDeleted,
        int Version,
        UpdatePostProjectionRequest_Content? Content,
        IEnumerable<Media> Media
    ) : IRequest;
    
}
