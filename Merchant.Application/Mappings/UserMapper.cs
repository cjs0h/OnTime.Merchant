using AutoMapper;
using Merchant.Application.DTOs.Users;
using Merchant.Application.Forms;
using Merchant.Application.Forms.Users;
using Merchant.Domain.Entities;

namespace Merchant.Application.Mappings;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.DateOfBirth,
                opt => opt.MapFrom(src => src.DateOfBirth.ToUniversalTime()))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            )
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UpdateUserForm, User>()
            .ForMember(dest => dest.DateOfBirth,
                opt => opt.MapFrom(src => src.DateOfBirth!.Value.ToUniversalTime()))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<RegisterForm, User>()
            .ForMember(dest => dest.DateOfBirth,
                opt => opt.MapFrom(src => src.DateOfBirth.ToUniversalTime()));
    }
}