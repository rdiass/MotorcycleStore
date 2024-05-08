using MotorcycleStore.WebApp.MVC.Models;
using System.Text.Json;

namespace MotorcycleStore.WebApp.MVC.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserResponseLogin> Login(UserLoginViewModel userViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:44306/api/auth/login", userViewModel);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync(), options);

        return result;
    }

    public async Task<UserResponseLogin> Register(UserViewModel userViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:44306/api/auth/create", userViewModel);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync(), options);

        return result;
    }
}
