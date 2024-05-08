using System.Security.Claims;

namespace MotorcycleStore.WebApp.MVC.Extensions;

public interface IUser
{
    string Name { get; }
    Guid GetUserId();
    string GetUserEmail();
    string GetUserToken();
    bool IsAuthenticated();
    bool HasRole(string role);
    IEnumerable<Claim> GetClaimsIdentity();
    HttpContext GetHttpContext();
}
