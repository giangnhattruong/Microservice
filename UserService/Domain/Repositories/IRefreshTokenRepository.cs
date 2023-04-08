using UserService.Domain.Models;

namespace UserService.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);

    Task<RefreshToken?> FindByTokenAsync(string token);

    void Update(RefreshToken token);
}