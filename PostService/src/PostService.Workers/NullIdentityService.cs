using PostService.Application;

namespace PostService.Workers
{
    internal class NullIdentityService : IIdentityService
    {
        public Guid UserId => throw new NotImplementedException();

        public bool IsAdmin => throw new NotImplementedException();

        public bool IsAdminOrOwner(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
