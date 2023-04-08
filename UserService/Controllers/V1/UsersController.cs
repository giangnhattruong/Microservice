using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserService.Contracts.V1;
using UserService.Domain.Models;
using UserService.Domain.Services;
using UserService.Resources;

namespace UserService.Controllers.V1;

public class UsersController : BaseApiController
{
    private readonly ILogger<UsersController> _logger;
    
    private readonly IUserService _userService;

    private readonly IMapper _mapper;

    public UsersController(
        ILogger<UsersController> logger,
        IUserService userService, 
        IMapper mapper)
    {
        _logger = logger;
        _logger.LogDebug(1, "NLog injected into UsersController");
        _userService = userService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<UserResource>> ListAsync()
    {
        var users = await _userService.ListAsync();
        return _mapper.Map<IEnumerable<UserResource>>(users);
    }

    [HttpPost(ApiRoutes.Users.Register)]
    public async Task<IActionResult> RegisterUserAsync([FromBody] SaveUserResource resource)
    {
        var user = _mapper.Map<User>(resource);
        
        var result = await _userService.RegisterAsync(user, resource.Password);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Resource);
    }

    [HttpPost(ApiRoutes.Users.Login)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginResource resource)
    {
        var result = await _userService.LoginAsync(resource.UserName, resource.Password);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Resource);
    }

    [HttpPost(ApiRoutes.Users.RefreshToken)]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenResource resource)
    {
        var result = await _userService.RefreshTokenAsync(resource.Token, resource.RefreshToken);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Resource);
    }
}