using AutoMapper;
using UserService.Domain.Models;
using UserService.Resources;

namespace UserService.Mapper;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, UserResource>();
    }
}