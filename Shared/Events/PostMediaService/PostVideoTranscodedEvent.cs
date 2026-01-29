namespace Shared.Events.PostMediaService
{
    public record PostVideoTranscodedEvent(Guid Id, string BlobName, string TranscodedBlobName);
}
