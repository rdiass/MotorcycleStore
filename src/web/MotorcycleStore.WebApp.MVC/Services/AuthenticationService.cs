using MotorcycleStore.WebApp.MVC.Models;

namespace MotorcycleStore.WebApp.MVC.Services;

public class AuthenticationService : Service, IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserResponseLogin> Login(UserLoginViewModel userViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:44306/api/auth/login", userViewModel);

        if (!TryError(response))
        {
            return new UserResponseLogin
            {
                ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
            };
        };

        return await DeserializeObjectResponse<UserResponseLogin>(response); ;
    }

    public async Task<UserResponseLogin> Register(UserViewModel userViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:44306/api/auth/create", userViewModel);

        if (!TryError(response))
        {
            return new UserResponseLogin
            {
                ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
            };
        };

        return await DeserializeObjectResponse<UserResponseLogin>(response);
    }
}
