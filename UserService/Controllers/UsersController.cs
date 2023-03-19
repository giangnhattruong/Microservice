using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Resources;
using UserService.Domain.Models;

namespace UserService.Controllers;

public class UsersController : BaseApiController
{
    private readonly UserManager<User> _userManager;

    private readonly IMapper _mapper;

    public UsersController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] SaveUserResource resource)
    {
        var user = new User() { UserName = resource.UserName, FullName = resource.UserName, Email = resource.Email };
        
        var result = await _userManager.CreateAsync(
            user, 
            resource.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var createdResource = new UserResource() {Id = user.Id, Email = user.Email, FullName = user.FullName, UserName = user.UserName};

        return Created("", createdResource);
    }
}