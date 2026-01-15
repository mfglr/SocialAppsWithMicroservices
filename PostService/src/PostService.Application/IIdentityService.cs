namespace PostService.Application
{
    public interface IIdentityService
    {
        Guid UserId { get; }
        bool IsAdmin { get; }
        bool IsAdminOrOwner(Guid userId);
    }
}
