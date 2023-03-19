using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Resources;
using UserService.Domain.Models;
using UserService.Domain.Services;

namespace UserService.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserService _userService;

    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<UserResource>> ListAsync()
    {
        var users = await _userService.ListAsync();
        return _mapper.Map<IEnumerable<UserResource>>(users);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync([FromBody] SaveUserResource resource)
    {
        var user = _mapper.Map<User>(resource);
        
        var result = await _userService.RegisterAsync(user, resource.Password);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Resource);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginResource resource)
    {
        var result = await _userService.LoginAsync(resource.UserName, resource.Password);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Resource);
    }
}