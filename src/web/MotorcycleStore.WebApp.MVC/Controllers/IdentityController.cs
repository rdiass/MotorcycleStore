using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MotorcycleStore.WebApp.MVC.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IAuthenticationService = MotorcycleStore.WebApp.MVC.Services.IAuthenticationService;

namespace MotorcycleStore.WebApp.MVC.Controllers;

public class IdentityController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public IdentityController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    [Route("new-account")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Route("new-account")]
    public async Task<IActionResult> Register(UserViewModel userViewModel)
    {
        if(!ModelState.IsValid) return View(userViewModel);

        var response = await _authenticationService.Register(userViewModel);

        await DoLogin(response);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserLoginViewModel userViewModel)
    {
        if(!ModelState.IsValid) return View(userViewModel);

        var response = await _authenticationService.Login(userViewModel);

        await DoLogin(response);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    private async Task DoLogin(UserResponseLogin response)
    {
        var token = GetFormattedToken(response.AccessToken);

        var claims = new List<Claim> 
        {
            new("JWT", response.AccessToken)
        };

        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    private static JwtSecurityToken GetFormattedToken(string jwtToken)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
    }
}
