using MotorcycleStore.Identity.API.Models;

namespace MotorcycleStore.Identity.API.Helpers;

public interface ITokenGenerator
{
    Task<UserResponseLogin> GenerateJwt(string email);
}
