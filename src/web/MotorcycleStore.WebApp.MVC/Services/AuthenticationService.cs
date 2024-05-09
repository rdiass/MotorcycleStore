using Microsoft.Extensions.Options;
using MotorcycleStore.WebApp.MVC.Extensions;
using MotorcycleStore.WebApp.MVC.Models;

namespace MotorcycleStore.WebApp.MVC.Services;

public class AuthenticationService : Service, IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient, IOptions<AppSettings> appSettings)
    {
        httpClient.BaseAddress = new Uri(appSettings.Value.AuthenticationUrl);
        _httpClient = httpClient;
    }

    public async Task<UserResponseLogin> Login(UserLoginViewModel userViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("login", userViewModel);

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
        var response = await _httpClient.PostAsJsonAsync("create", userViewModel);

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
