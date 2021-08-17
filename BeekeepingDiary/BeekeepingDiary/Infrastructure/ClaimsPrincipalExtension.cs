using System.Security.Claims;

using static BeekeepingDiary.Areas.Admin.AdminConstants;

namespace BeekeepingDiary.Infrastructure
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
    => user.IsInRole(AdministratorRoleName);
    }

}
