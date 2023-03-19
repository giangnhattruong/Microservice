using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using OrderService.DTOs;
using OrderService.Services.Communication;

namespace OrderService.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IModelToDtoMapper<Product, ProductDto> _productMapper;
    private readonly IDtoToModelMapper<SaveProductDto, Product> _saveProductMapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(
        IProductRepository productRepository, 
        IModelToDtoMapper<Product, ProductDto> productMapper, 
        IDtoToModelMapper<SaveProductDto, Product> saveProductMapper, 
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _productMapper = productMapper;
        _saveProductMapper = saveProductMapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<ProductDto>?> ListAsync()
    {
        var products = await _productRepository.ListAsync();
        if (products == null) return null;
        return _productMapper.ToListDtos(products);
    }
    
    public async Task<BaseResponse<ProductDto>> AddAsync(SaveProductDto product)
    {
        try
        {
            var productModel = _saveProductMapper.ToModel(product);
            await _productRepository.AddAsync(productModel);
            
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<ProductDto>(_productMapper.ToDto(productModel));
        }
        catch (Exception ex)
        {
            return new BaseResponse<ProductDto>($"An error occurred: {ex.Message}");
        }
    }
    
    public async Task<BaseResponse<ProductDto>> UpdateAsync(int id, SaveProductDto product)
    {
        var existingProduct = await _productRepository.GetAsync(id);

        if (existingProduct == null)
            return new BaseResponse<ProductDto>("Data not found.");

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;

        try
        {
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<ProductDto>(_productMapper.ToDto(existingProduct));
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new BaseResponse<ProductDto>($"An error occurred: {ex.Message}");
        }
    }
    
    public async Task<BaseResponse<ProductDto>> RemoveAsync(int id)
    {
        var existingProduct = await _productRepository.GetAsync(id);

        if (existingProduct == null)
            return new BaseResponse<ProductDto>("Data not found.");

        try
        {
            _productRepository.Remove(existingProduct);
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<ProductDto>(_productMapper.ToDto(existingProduct));
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new BaseResponse<ProductDto>($"An error occurred: {ex.Message}");
        }
    }
    
}