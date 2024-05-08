using MotorcycleStore.WebApp.MVC.Models;

namespace MotorcycleStore.WebApp.MVC.Services;

public interface IAuthenticationService
{
    Task<UserResponseLogin> Login(UserLoginViewModel userViewModel);

    Task<UserResponseLogin> Register(UserViewModel userViewModel);
}
