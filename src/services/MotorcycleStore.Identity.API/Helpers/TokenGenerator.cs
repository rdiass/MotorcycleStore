using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MotorcycleStore.Identity.API.Models;
using MotorcycleStore.Identity.API.Settings;
using MS.Identity.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MotorcycleStore.Identity.API.Helpers;

public class TokenGenerator : ITokenGenerator
{
    private UserManager<ApplicationUser> _userManager;
    private readonly AppSettings _appSettings;

    public TokenGenerator(UserManager<ApplicationUser> userManager, IOptions<AppSettings> appSettings)
    {
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    public async Task<UserResponseLogin> GenerateJwt(string email)
    {
        var appUser = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(appUser);

        var identityClaims = await GetUserClaims(appUser, claims);
        var encodedToken = TokenEncode(identityClaims);
        var response = GetResponseToken(encodedToken, appUser, claims);
        return response;
    }

    private UserResponseLogin GetResponseToken(string encodedToken, ApplicationUser appUser, IEnumerable<Claim> claims)
    {
        return new UserResponseLogin
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.HoursToExpire).TotalSeconds,
            UserToken = new UserToken
            {
                Id = appUser.Id.ToString(),
                Email = appUser.Email,
                Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
            }
        };
    }

    private string TokenEncode(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.HoursToExpire),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }

    private async Task<ClaimsIdentity> GetUserClaims(ApplicationUser appUser, ICollection<Claim> claims)
    {
        var userRoles = await _userManager.GetRolesAsync(appUser);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, appUser.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, appUser.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));

        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return identityClaims;
    }

}
