using Application.CQRS.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using static Application.CQRS.Users.Handlers.Register;

namespace Application.Automapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        //CreateMap<UpdateDto, User>().ReverseMap();
        CreateMap<RegisterUserDto, User>().ReverseMap();
        CreateMap<User, GetUserByIdDto>().ForMember(dest=>dest.Role,opt=>opt.MapFrom(src=>src.UserType.ToString())).ForMember(dest=>dest.Role,func=>func.MapFrom(src=>src.Gender.ToString()));  //burda role ve genderi string e cevirmeliyik

        CreateMap<RegisterCommand,User>()
        .ForMember(dest=>dest.UserType,opt=>opt.MapFrom(src=>ConvertToRole(src.Role)))
        .ForMember(dest=>dest.Gender,opt=>opt.MapFrom(src=> ConverToGender(src.Gender))); 
    }


    public static Role ConvertToRole(string role)
    {
        return Enum.TryParse<Role>(role,true,out var roleType)?roleType:throw new ArgumentException($"Invalid role name {role}");
    }

    public static Gender ConverToGender(string gender)
    {
        return Enum.TryParse(gender, true, out Gender result) ? result : throw new ArgumentException($"Wrong gender Name {gender}");
    }
}
