using AutoMapper;
using ProductService.Domain.Models;
using ProductService.Resources;

namespace ProductService.Mapper;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveCategoryResource, Category>();

        CreateMap<SaveProductResource, Product>();
    }
}