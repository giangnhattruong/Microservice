using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserService.Domain.Models;
using UserService.Resources;

namespace UserService.Mapper;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<UserResource, User>();
    }
}