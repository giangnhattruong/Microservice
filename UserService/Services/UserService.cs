using System.Security.Claims;
using System.Text;
using AutoMapper;
using Grpc.Core;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using UserService.Domain.Models;
using UserService.Domain.Repositories;
using UserService.Domain.Services;
using UserService.Grpc;
using UserService.Options;
using UserService.Resources;
using UserService.Services.Communication;

namespace UserService.Services;

public class UserService : UserGrpc.UserGrpcBase, IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IUserRepository _userRepository;

    private readonly ITokenService _tokenService;

    private readonly IMessageBrokerService _mqService;

    private readonly IRefreshTokenRepository _refreshTokenRepository;

    private readonly IMapper _mapper;

    public UserService(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        ITokenService tokenService,
        IMessageBrokerService mqService,
        IRefreshTokenRepository refreshTokenRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _mqService = mqService;
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public override async Task<ListUserGrpcResponse> List(Empty request, ServerCallContext context)
    {
        var users = await _userRepository.ListAsync();

        var grpcResponseUserList = new ListUserGrpcResponse();
        grpcResponseUserList.UserList.AddRange(users.Select(u => new UserGrpcResponse()
        {
            Id = u.Id,
            Name = u.FullName
        }));

        return grpcResponseUserList;
    }

    public async Task<BaseResponse<AuthTokenResource>> RegisterAsync(SaveUserResource resource)
    {
        try
        {
            var user = _mapper.Map<User>(resource);
            
            var existingUser = await _userRepository.FindAsync(user.UserName);

            if (existingUser != null)
                return new BaseResponse<AuthTokenResource>("User already existed.");
            
            await _userRepository.RegisterAsync(user, resource.Password);
            
            SendMessage(new GeneralUserResource() {Id = user.Id, FullName = user.FullName});

            var token = await _tokenService.CreateTokenAsync(user);
            
            return new BaseResponse<AuthTokenResource>(token);
        }
        catch (Exception ex)
        {
            return new BaseResponse<AuthTokenResource>($"An error occur when registering user: {ex.Message},\n Trace: {ex.StackTrace}");
        }
    }

    private void SendMessage(GeneralUserResource user)
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

            var token = await _tokenService.CreateTokenAsync(authenticatedUser);

            return new BaseResponse<AuthTokenResource>(token);
        }
        catch (Exception ex)
        {
            return new BaseResponse<AuthTokenResource>($"An error occur when logging: {ex.Message},\n Trace: {ex.StackTrace}");
        }
    }

    public async Task<BaseResponse<AuthTokenResource>> RefreshTokenAsync(string token, string refreshToken)
    {
        try
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            
            if (principal == null)
                return new BaseResponse<AuthTokenResource>("Invalid token");

            var tokenExpiryUnix = long.Parse(principal.Claims.Single(p => p.Type == JwtRegisteredClaimNames.Exp).Value);
            var tokenExpiryDate = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(tokenExpiryUnix);

            if (tokenExpiryDate > DateTime.Now)
                return new BaseResponse<AuthTokenResource>("The access token has not expired yet.");

            var jti = principal.Claims.Single(p => p.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _refreshTokenRepository.FindByTokenAsync(refreshToken);

            if
            (
                storedRefreshToken == null ||
                storedRefreshToken.JwtId != jti ||
                storedRefreshToken.ExpiryDate < DateTime.Now ||
                storedRefreshToken.Invalidated ||
                storedRefreshToken.Used
            )
                return new BaseResponse<AuthTokenResource>("Invalid refresh token.");

            storedRefreshToken.Used = true;
            _refreshTokenRepository.Update(storedRefreshToken);
            await _unitOfWork.CompleteAsync();

            var email = principal.Claims.Single(p => p.Type == ClaimTypes.Email).Value;
            var user = await _userRepository.FindByEmailAsync(email);

            var resource = await _tokenService.CreateTokenAsync(user);
            return new BaseResponse<AuthTokenResource>(resource);
        }
        catch (Exception ex)
        {
            return new BaseResponse<AuthTokenResource>($"An error occur when logging: {ex.Message},\n Trace: {ex.StackTrace}");
        }
    }
}