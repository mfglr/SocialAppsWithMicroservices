namespace Shared.Events.UserService
{
    public record UserMediaThumbnailGeneratedEvent(Guid Id, string BlobName, Thumbnail Thumbnail);
}
