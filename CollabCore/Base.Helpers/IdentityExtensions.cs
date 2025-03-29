using System.Security.Claims;

namespace Base.Helpers;

public static class IdentityExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userIdStr = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userId = Guid.Parse(userIdStr);

        return userId;
    }
}