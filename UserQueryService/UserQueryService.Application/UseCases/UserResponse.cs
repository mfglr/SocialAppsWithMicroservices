using Shared.Events;

namespace UserQueryService.Application.UseCases
{

    public record UserResponse_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        bool IsDeleted
    );
    public record UserResponse(
        string Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        string? Name,
        string UserName,
        string Gender,
        IEnumerable<UserResponse_Media> Media
    );
}
