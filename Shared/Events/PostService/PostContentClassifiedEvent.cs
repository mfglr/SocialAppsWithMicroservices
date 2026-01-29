namespace Shared.Events.PostService
{
    public record PostContentClassifiedEvent(Guid Id, ModerationResult ModerationResult);
}
