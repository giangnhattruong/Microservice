﻿using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Mapper;
using OrderService.Domain.Services;
using OrderService.DTOs;

namespace OrderService.Controllers;

public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>?> ListAsync()
    {
        return await _orderService.ListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] SaveOrderDto dto)
    {
        var response = await _orderService.AddAsync(dto);

        if (!response.Success)
            return BadRequest(new ErrorDto(response.Message));

        return Ok(response.Resource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveOrderDto dto)
    {
        var response = await _orderService.UpdateAsync(id, dto);

        if (!response.Success)
            return BadRequest(new ErrorDto(response.Message));

        return Ok(response.Resource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> AddAsync(int id)
    {
        var response = await _orderService.RemoveAsync(id);

        if (!response.Success)
            return BadRequest(new ErrorDto(response.Message));

        return Ok(response.Resource);
    }
}