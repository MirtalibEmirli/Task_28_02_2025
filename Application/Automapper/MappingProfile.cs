using Application.CQRS.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Automapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        //CreateMap<UpdateDto, User>().ReverseMap();
        CreateMap<RegisterUserDto,User>().ReverseMap(); 
    }
}
