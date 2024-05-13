using MotorcycleStore.WebAPI.Core.Models;

namespace MotorcycleStore.WebApp.MVC.Models;
public class UserResponseLogin
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; }
    public ResponseResult ResponseResult { get; set; }
}
