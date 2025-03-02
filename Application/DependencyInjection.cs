﻿using Application.Automapper;
using Application.PipelineBehavior;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class DependencyInjection
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services) {

        //automapper
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();   
        services.AddSingleton(mapper); //fluent validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //mediatR
        services.AddMediatR(Assembly.GetExecutingAssembly());

        //pipelinebehavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));


        return services;


    }
}
