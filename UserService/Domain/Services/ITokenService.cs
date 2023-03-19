using UserService.Domain.Models;
using UserService.Resources;

namespace UserService.Domain.Services;

public interface ITokenService
{
    AuthTokenResource CreateToken(User user);
}