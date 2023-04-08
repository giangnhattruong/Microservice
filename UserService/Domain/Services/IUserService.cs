using UserService.Domain.Models;
using UserService.Resources;
using UserService.Services.Communication;

namespace UserService.Domain.Services;

public interface IUserService
{
    Task<ICollection<User>> ListAsync();

    Task<BaseResponse<AuthTokenResource>> RegisterAsync(User user, string password);

    Task<BaseResponse<AuthTokenResource>> LoginAsync(string userName, string password);
    
    Task<BaseResponse<AuthTokenResource>> RefreshTokenAsync(string token, string refreshToken);
}