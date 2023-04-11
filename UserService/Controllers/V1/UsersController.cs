using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserService.Contracts.V1;
using UserService.Contracts.V1.Request;
using UserService.Contracts.V1.Response;
using UserService.Domain.Models;
using UserService.Domain.Services;
using UserService.Resources;
using UserService.Validators;

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
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest request)
    {
        var validator = new RegisterUserRequestValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            var errorResponse = new ErrorResponse();
            
            errorResponse.Errors = validationResult.Errors
                .Select(e => new ErrorModel()
                {
                    Field = e.PropertyName,
                    Messages = new List<string>() { e.ErrorMessage }
                }).ToList();

             return BadRequest(errorResponse.Errors);
        }
        
        var resource = _mapper.Map<SaveUserResource>(request);

        var result = await _userService.RegisterAsync(resource);

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