using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Services;
using OrderService.DTOs;

namespace OrderService.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductDto>?> ListAsync()
    {
        return await _productService.ListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveProductDto dto)
    {
        var result = await _productService.AddAsync(dto);

        if (!result.Success)
            return BadRequest(new ErrorDto(result.Message));

        return Ok(result.Resource);
    }
}