using AutoMapper;
using UserService.Contracts.V1.Request;
using UserService.Resources;

namespace UserService.Mapper;

public class RequestToResourceProfile : Profile
{
    public RequestToResourceProfile()
    {
        CreateMap<RegisterUserRequest, SaveUserResource>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
    }
}