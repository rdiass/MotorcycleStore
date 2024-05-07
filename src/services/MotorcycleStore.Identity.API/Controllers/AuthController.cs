using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotorcycleStore.Identity.API.Models;
using MS.Identity.API.Models;

namespace MS.Identity.API.Controllers;

[Route("api/auth")]
public class AuthController : Controller
{
    private UserManager<ApplicationUser> _userManager;
    private SignInManager<ApplicationUser> _signInManager;

    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(User user)
    {
        if (!ModelState.IsValid) return BadRequest();

        var appUser = new ApplicationUser
        {
            UserName = user.Name,
            Email = user.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(appUser, user.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(appUser, isPersistent: false);
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin user)
    {
        if (!ModelState.IsValid) return BadRequest();

        var appUser = await _userManager.FindByEmailAsync(user.Email);

        if(appUser == null)
        {
            return BadRequest();
        }

        var result = await _signInManager.PasswordSignInAsync(appUser, user.Password, isPersistent: false, lockoutOnFailure: false);

        if(result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}
