using PostService.Application;
using System.Security.Claims;

namespace PostService.Api
{
    public class IdentityService(IHttpContextAccessor httpContextAccessor) : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Guid UserId =>
            Guid.Parse(
                _httpContextAccessor
                    .HttpContext?
                    .User
                    .Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
                    .Value ??
                string.Empty
            );

        public bool IsAdmin =>
            _httpContextAccessor
                .HttpContext?
                .User
                .Claims
                .Any(x => x.Type == ClaimTypes.Role && x.Value == "admin") ?? false;

        public bool IsAdminOrOwner(Guid userId) => IsAdmin || UserId == userId;
    }
}
