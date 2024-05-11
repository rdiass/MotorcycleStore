using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MotorcycleStore.WebAPI.Core.Identity;

public partial class CustomAuthorize
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequirementClaimFilter))
        {
            Arguments = [new Claim(claimName, claimValue)];
        }
    }
}
