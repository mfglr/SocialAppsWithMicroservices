namespace Shared.Events.PostMediaService
{
    public record PostMediaClassifiedEvent(Guid Id, string ContainerName, string BlobName, MediaType Type, ModerationResult ModerationResult);
}
