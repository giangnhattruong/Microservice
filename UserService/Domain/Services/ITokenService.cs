using System.Security.Claims;
using UserService.Domain.Models;
using UserService.Resources;

namespace UserService.Domain.Services;

public interface ITokenService
{
    Task<AuthTokenResource> CreateTokenAsync(User user);

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
}