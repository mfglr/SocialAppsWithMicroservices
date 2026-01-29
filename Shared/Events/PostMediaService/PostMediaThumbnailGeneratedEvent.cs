namespace Shared.Events.PostMediaService
{
    public record PostMediaThumbnailGeneratedEvent(Guid Id, string BlobName, Thumbnail Thumbnail);
}
