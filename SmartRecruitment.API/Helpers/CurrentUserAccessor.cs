using System.Security.Claims;

namespace SmartRecruitment.API.Helpers
{
    public class CurrentUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserAccessor(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetCurrentUserId()
        {
            string? value = _httpContextAccessor
                .HttpContext?
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(value, out int userId)
                ? userId
                : null;
        }

        public string? GetCurrentUserRole()
        {
            return _httpContextAccessor
                .HttpContext?
                .User
                .FindFirstValue(ClaimTypes.Role);
        }

        public string? GetCurrentUserEmail()
        {
            return _httpContextAccessor
                .HttpContext?
                .User
                .FindFirstValue(ClaimTypes.Email);
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor
                .HttpContext?
                .User
                .Identity?
                .IsAuthenticated ?? false;
        }
    }
}
