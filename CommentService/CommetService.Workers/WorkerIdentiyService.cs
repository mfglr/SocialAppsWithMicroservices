using CommentService.Application;

namespace CommetService.Workers
{
    internal class WorkerIdentiyService : IIdentityService
    {
        public Guid UserId => Guid.NewGuid();
    }
}
