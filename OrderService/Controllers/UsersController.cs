using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Services;
using OrderService.DTOs;

namespace OrderService.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>?> ListAsync()
    {
        return await _userService.ListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveUserDto dto)
    {
        var result = await _userService.AddAsync(dto);

        if (!result.Success)
            return BadRequest(new ErrorDto(result.Message));

        return Ok(result.Resource);
    }
}