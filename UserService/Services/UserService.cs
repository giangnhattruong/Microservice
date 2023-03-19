using Microsoft.VisualBasic;
using UserService.Domain.Models;
using UserService.Domain.Repositories;
using UserService.Domain.Services;
using UserService.Persistence.Contexts;
using UserService.Resources;
using UserService.Services.Communication;

namespace UserService.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IUserRepository _userRepository;

    private readonly ITokenService _tokenService;

    public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<ICollection<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<BaseResponse<AuthTokenResource>> RegisterAsync(User user, string password)
    {
        try
        {
            var existingUser = await _userRepository.FindAsync(user.UserName);

            if (existingUser != null)
                return new BaseResponse<AuthTokenResource>("User already existed.");
            
            await _userRepository.RegisterAsync(user, password);

            var token = _tokenService.CreateToken(user);
            
            return new BaseResponse<AuthTokenResource>(token);
        }
        catch (Exception ex)
        {
            return new BaseResponse<AuthTokenResource>($"An error occur when registering user: {ex.Message}");
        }
    }

    public async Task<BaseResponse<AuthTokenResource>> LoginAsync(string userName, string password)
    {
        try
        {
            var authenticatedUser = await _userRepository.FindAsync(userName, password);

            if (authenticatedUser == null)
                return new BaseResponse<AuthTokenResource>("Invalid credentials.");

            var token = _tokenService.CreateToken(authenticatedUser);

            return new BaseResponse<AuthTokenResource>(token);
        }
        catch (Exception ex)
        {
            return new BaseResponse<AuthTokenResource>($"An error occur when logging: {ex.Message},\n Trace: {ex.StackTrace}");
        }
    }
}