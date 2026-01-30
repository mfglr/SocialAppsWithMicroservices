using MediatR;
using Shared.Events;

namespace PostQueryService.Application.UseCases.SetUser
{
    public record SetUserRequest_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails
    );
    public record SetUserRequest(
        Guid UserId,
        string UserName,
        string? Name,
        SetUserRequest_Media ProfilePhoto,
        int Version
    ) : IRequest;
}
