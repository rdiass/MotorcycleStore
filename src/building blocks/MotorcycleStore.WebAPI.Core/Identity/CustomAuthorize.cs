using Microsoft.AspNetCore.Http;

namespace MotorcycleStore.WebAPI.Core.Identity;

public partial class CustomAuthorize
{
    public static bool ValidateClaimsUser(HttpContext context, string claimName, string claimValue)
    {
        return context.User.Identity.IsAuthenticated &&
               context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
    }
}
