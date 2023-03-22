using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Domain.Models;
using ProductService.Domain.Services;
using ProductService.Resources;

namespace ProductService.Domain.Controllers;

public class CategoriesController : BaseApiController
{
    private readonly ILogger<CategoriesController> _logger;
    
    private readonly IMapper _mapper;

    private readonly ICategoryService _categoryService;

    public CategoriesController(
        ILogger<CategoriesController> logger,
        IMapper mapper, 
        ICategoryService categoryService)
    {
        _logger = logger;
        _logger.LogDebug(1, "NLog injected into HomeController");
        _mapper = mapper;
        _categoryService = categoryService;
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<CategoryResource>> ListAsync()
    {
        var categories = await _categoryService.ListAsync();
        var resources = _mapper.Map<ICollection<Category>, ICollection<CategoryResource>>(categories);

        return resources;
    }
    
    /// <summary>
    /// Add a new category
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] SaveCategoryResource resource)
    {
        var category = _mapper.Map<SaveCategoryResource, Category>(resource);

        var response = await _categoryService.AddAsync(category);
        
        if (!response.Success)
            return BadRequest(new ErrorResource(response.Message));

        var categoryResource = _mapper.Map<Category, CategoryResource>(response.Resource);
        
        return Ok(categoryResource);
    }

    /// <summary>
    /// Update a category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveCategoryResource resource)
    {
        var category = _mapper.Map<SaveCategoryResource, Category>(resource);

        var response = await _categoryService.UpdateAsync(id, category);

        if (!response.Success)
            return BadRequest(new ErrorResource(response.Message));

        var categoryResource = _mapper.Map<Category, CategoryResource>(response.Resource);
        
        return Ok(categoryResource);
    }
    
    /// <summary>
    /// Remove a category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        var response = await _categoryService.RemoveAsync(id);

        if (!response.Success)
            return BadRequest(new ErrorResource(response.Message));

        var categoryResource = _mapper.Map<Category, CategoryResource>(response.Resource);
        
        return Ok(categoryResource);
    }
}