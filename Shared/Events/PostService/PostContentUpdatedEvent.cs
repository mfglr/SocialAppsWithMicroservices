namespace Shared.Events.PostService
{
    public record PostContentUpdatedEvent(Guid Id,string Content);
}
