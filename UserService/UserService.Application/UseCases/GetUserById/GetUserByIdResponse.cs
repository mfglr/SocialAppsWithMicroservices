using Shared.Events;

namespace UserService.Application.UseCases.GetUserById
{
    public record GetUserByIdResponse_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        bool IsDeleted
    );

    public record GetUserByIdResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int Version,
        bool IsDeleted,
        string? Name,
        string Username,
        string Gender,
        IEnumerable<GetUserByIdResponse_Media> Media
    );
}
