using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Mapper;
using OrderService.Domain.Services;
using OrderService.DTOs;
using UserService.Grpc;

namespace OrderService.Controllers;

public class OrdersController : BaseApiController
{
    private readonly ILogger<OrdersController> _logger;
    
    private readonly IOrderService _orderService;

    public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
    {
        _logger = logger;
        _logger.LogDebug(1, "NLog injected into HomeController");
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>?> ListAsync()
    {
        _logger.LogInformation("HEY! YO!");
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
    
    [HttpPost("test-grpc")]
    public async Task<IActionResult> TestGrpcAsync()
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8081", new GrpcChannelOptions()
        {
            HttpClient = new HttpClient(new HttpClientHandler())
        });
        var client = new UserGrpc.UserGrpcClient(channel);

        var grpcResponse = await client.ListAsync(new Empty());
        
        return Ok(grpcResponse);
    }
}