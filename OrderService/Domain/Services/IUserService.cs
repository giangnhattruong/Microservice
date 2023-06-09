﻿using OrderService.Domain.Models;
using OrderService.DTOs;
using OrderService.Services.Communication;

namespace OrderService.Domain.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>?> ListAsync();
    Task<BaseResponse<UserDto>> AddAsync(SaveUserDto user);
    Task<BaseResponse<UserDto>> UpdateAsync(string id, SaveUserDto user);
    Task<BaseResponse<UserDto>> RemoveAsync(string id);
}