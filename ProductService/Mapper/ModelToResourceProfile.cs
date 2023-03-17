using AutoMapper;
using ProductService.Domain.Models;
using ProductService.Resources;

namespace ProductService.Mapper;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Category, CategoryResource>();

        CreateMap<Product, ProductResource>();
    }
}