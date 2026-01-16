using PostService.Application;

namespace PostService.Workers
{
    internal class NullIdentityService : IIdentityService
    {
        public Guid UserId => Guid.NewGuid();

        public bool IsAdmin => false;

        public bool IsAdminOrOwner(Guid userId) => false;
    }
}
