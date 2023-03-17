using ProductService.Domain.Models;
using ProductService.Domain.Repositories;
using ProductService.Domain.Services;
using ProductService.Services.Communication;

namespace ProductService.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IProductRepository _productRepository;
    
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }
    
    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns></returns>
    public async Task<ICollection<Product>> ListAsync()
    {
        return await _productRepository.ListAsync();
    }

    /// <summary>
    /// Get product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<BaseResponse<Product>> FindByIdAsync(int id)
    {
        try
        {
            var product = await _productRepository.FindByIdAsync(id);

            if (product == null)
                return new BaseResponse<Product>("Product not found.");

            return new BaseResponse<Product>(product);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Product>($"An error occurred when saving the product: {ex.Message}");
        }
    }

    /// <summary>
    /// Add new product
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task<BaseResponse<Product>> AddAsync(Product product)
    {
        try
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
            if (existingCategory == null)
                return new BaseResponse<Product>("Invalid category.");

            await _productRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<Product>(product);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Product>($"An error occurred when saving the product: {ex.Message}");
        }
    }

    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="id"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task<BaseResponse<Product>> UpdateAsync(int id, Product product)
    {
        try
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new BaseResponse<Product>("Product not found.");
            
            var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
            if (existingCategory == null)
                return new BaseResponse<Product>("Invalid category.");

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.CategoryId = product.CategoryId;
            
            _productRepository.Update(existingProduct);
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<Product>(existingProduct);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Product>($"An error occurred when updating the product: {ex.Message}");
        }
    }

    /// <summary>
    /// Remove a product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<BaseResponse<Product>> RemoveAsync(int id)
    {
        try
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new BaseResponse<Product>("Product not found.");
            
            _productRepository.Remove(existingProduct);
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<Product>(existingProduct);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Product>($"An error occurred when deleting the product: {ex.Message}");
        }
    }
}