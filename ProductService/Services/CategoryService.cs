using ProductService.Domain.Models;
using ProductService.Domain.Repositories;
using ProductService.Domain.Services;
using ProductService.Services.Communication;

namespace ProductService.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }
    
    /// <summary>
    /// Get all categories
    /// </summary>
    /// <returns></returns>
    public async Task<ICollection<Category>> ListAsync()
    {
        return await _categoryRepository.ListAsync();
    }

    /// <summary>
    /// Get category by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<BaseResponse<Category>> FindByIdAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.FindByIdAsync(id);

            if (category == null)
                return new BaseResponse<Category>("Category not found.");

            return new BaseResponse<Category>(category);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Category>($"An error occurred when getting the category: {ex.Message}");
        }
    }

    /// <summary>
    /// Add new category
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task<BaseResponse<Category>> AddAsync(Category category)
    {
        try
        {
            await _categoryRepository.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<Category>(category);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Category>($"An error occurred when saving the category: {ex.Message}");
        }
    }

    /// <summary>
    /// Update an category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task<BaseResponse<Category>> UpdateAsync(int id, Category category)
    {
        try
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new BaseResponse<Category>("Category not found.");

            existingCategory.Name = category.Name;
            _unitOfWork.CompleteAsync();

            return new BaseResponse<Category>(existingCategory);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Category>($"An error occurred when update the category: {ex.Message}");
        }
    }

    /// <summary>
    /// Delete category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<BaseResponse<Category>> RemoveAsync(int id)
    {
        try
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new BaseResponse<Category>("Category not found.");

            _categoryRepository.Remove(existingCategory);
            _unitOfWork.CompleteAsync();

            return new BaseResponse<Category>(existingCategory);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Category>($"An error occurred when deleting the category: {ex.Message}");
        }
    }
}