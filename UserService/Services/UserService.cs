using System.Text;
using Newtonsoft.Json;
using UserService.Domain.Models;
using UserService.Domain.Repositories;
using UserService.Domain.Services;
using UserService.Resources;
using UserService.Services.Communication;

namespace UserService.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IUserRepository _userRepository;

    private readonly ITokenService _tokenService;

    private readonly IMessageBrokerService _mqService;

    public UserService(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        ITokenService tokenService,
        IMessageBrokerService mqService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _mqService = mqService;
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
            
            SendMessage(user);

            var token = _tokenService.CreateToken(user);
            
            return new BaseResponse<AuthTokenResource>(token);
        }
        catch (Exception ex)
        {
            return new BaseResponse<AuthTokenResource>($"An error occur when registering user: {ex.Message},\n Trace: {ex.StackTrace}");
        }
    }

    private void SendMessage(User user)
    {
        using var connection = _mqService.CreateChannel();
        using var model = connection.CreateModel();
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user));
        model.BasicPublish("UserExchange",
            string.Empty,
            true,
            basicProperties: null,
            body: body);
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