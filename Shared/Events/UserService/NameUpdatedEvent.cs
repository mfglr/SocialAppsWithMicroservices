namespace Shared.Events.UserService
{
    public record NameUpdatedEvent_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails
    );

    public record NameUpdatedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int Version,
        bool IsDeleted,
        string? Name,
        string Username,
        string Gender,
        IEnumerable<NameUpdatedEvent_Media> Media
    );
}
