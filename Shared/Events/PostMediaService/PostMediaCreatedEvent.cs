namespace Shared.Events.PostMediaService
{
    public record PostMediaCreatedEvent(Guid Id, string ContainerName, string BlobName, MediaType Type);
}
