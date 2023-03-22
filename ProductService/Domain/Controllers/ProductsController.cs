using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Domain.Models;
using ProductService.Domain.Services;
using ProductService.Resources;

namespace ProductService.Domain.Controllers;

public class ProductsController : BaseApiController
{
    private readonly ILogger<ProductsController> _logger;
    
    private readonly IMapper _mapper;

    private readonly IProductService _productService;

    public ProductsController(
        ILogger<ProductsController> logger,
        IMapper mapper, 
        IProductService productService)
    {
        _logger = logger;
        _logger.LogDebug(1, "NLog injected into HomeController");
        _mapper = mapper;
        _productService = productService;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<ProductResource>> ListAsync()
    {
        var products = await _productService.ListAsync();
        var resources = _mapper.Map<ICollection<Product>, ICollection<ProductResource>>(products);

        return resources;
    }

    /// <summary>
    /// Find product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> FindByIdAsync(int id)
    {
        var response = await _productService.FindByIdAsync(id);

        if (!response.Success)
            return BadRequest(new ErrorResource(response.Message));
        
        var resource = _mapper.Map<Product, ProductResource>(response.Resource);

        return Ok(resource);
    }
    
    /// <summary>
    /// Add a new product
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] SaveProductResource resource)
    {
        var product = _mapper.Map<SaveProductResource, Product>(resource);

        var response = await _productService.AddAsync(product);
        
        if (!response.Success)
            return BadRequest(new ErrorResource(response.Message));

        var productResource = _mapper.Map<Product, ProductResource>(response.Resource);
        
        return Ok(productResource);
    }

    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="id"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveProductResource resource)
    {
        var product = _mapper.Map<SaveProductResource, Product>(resource);

        var response = await _productService.UpdateAsync(id, product);

        if (!response.Success)
            return BadRequest(new ErrorResource(response.Message));

        var productResource = _mapper.Map<Product, ProductResource>(response.Resource);
        
        return Ok(productResource);
    }
    
    /// <summary>
    /// Remove a product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        var response = await _productService.RemoveAsync(id);

        if (!response.Success)
            return BadRequest(new ErrorResource(response.Message));

        var productResource = _mapper.Map<Product, ProductResource>(response.Resource);
        
        return Ok(productResource);
    }
}