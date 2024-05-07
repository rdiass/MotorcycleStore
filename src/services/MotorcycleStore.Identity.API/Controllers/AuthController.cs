using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotorcycleStore.Identity.API.Controllers;
using MotorcycleStore.Identity.API.Helpers;
using MotorcycleStore.Identity.API.Models;
using MS.Identity.API.Models;

namespace MS.Identity.API.Controllers;

[Route("api/auth")]
public class AuthController : MainController
{
    private UserManager<ApplicationUser> _userManager;
    private SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(User user)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var appUser = new ApplicationUser
        {
            UserName = user.Email,
            Email = user.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(appUser, user.Password);

        if (result.Succeeded)
        {
            return CustomResponse(await _tokenGenerator.GenerateJwt(user.Email));
        }

        foreach (var error in result.Errors)
        {
            AddError(error.Description);
        }

        return CustomResponse();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin user)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var appUser = await _userManager.FindByEmailAsync(user.Email);

        if(appUser == null)
        {
            return BadRequest();
        }

        var result = await _signInManager.PasswordSignInAsync(appUser, user.Password, isPersistent: false, lockoutOnFailure: true);

        if(result.Succeeded)
        {
            return CustomResponse(await _tokenGenerator.GenerateJwt(user.Email));
        }
        
        if(result.IsLockedOut)
        {
            AddError("User temporarily blocked after multiple attempts");
            return CustomResponse();
        }

        AddError("Incorrect user or password");
        return CustomResponse();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}
