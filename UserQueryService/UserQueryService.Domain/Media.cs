using UserService.Domain;

namespace UserQueryService.Domain
{
    public record Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        bool IsDeleted
    );
}
