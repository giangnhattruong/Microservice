using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using UserService.Domain.Models;
using UserService.Domain.Repositories;
using UserService.Domain.Services;
using UserService.Options;
using UserService.Resources;

namespace UserService.Services;

public class JwtService : ITokenService
{
    private readonly ILogger<JwtService> _logger;
    
    private readonly JwtSettings _jwtSettings;

    private readonly IRefreshTokenRepository _refreshTokenRepository;

    private readonly IUnitOfWork _unitOfWork;

    public JwtService(
        ILogger<JwtService> logger,
        JwtSettings jwtSettings, 
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork
        )
    {
        _logger = logger;
        _jwtSettings = jwtSettings;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthTokenResource> CreateTokenAsync(User user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenValidityInMinutes);

        var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials(),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        var refreshToken = new RefreshToken
        {
            Token = GenerateRefreshToken(),
            JwtId = token.Id,
            UserId = user.Id,
            CreationDate = DateTime.Now,
            ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenValidityInDays)
        };

        await _refreshTokenRepository.AddAsync(refreshToken);
        await _unitOfWork.CompleteAsync();

        return new AuthTokenResource
        {
            Token = tokenHandler.WriteToken(token),
            ExpiredAt = expiration,
            RefreshToken = refreshToken.Token
        };
    }
    
    private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
        new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private Claim[] CreateClaims(User user) =>
        new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _jwtSettings.Subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

    private SigningCredentials CreateSigningCredentials() =>
        new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)
            ),
            SecurityAlgorithms.HmacSha256
        );
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        try
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = _jwtSettings.Audience,
                ValidIssuer = _jwtSettings.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal =
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}