using MediatR;
using Shared.Events;

namespace UserQueryService.Application.UseCases.UpsertUser
{
    public record UpsertUserRequest_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        bool IsDeleted
    );
    public record UpsertUserRequest(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int Version,
        bool IsDeleted,
        string? Name,
        string Username,
        string Gender,
        IEnumerable<UpsertUserRequest_Media> Media
    ) : IRequest;
}
