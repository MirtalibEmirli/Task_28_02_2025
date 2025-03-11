using Application.CQRS.Users.DTOs;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.Handlers;

public class GetAllUsers
{

    public class GetAllCommand : IRequest<ResponseModelPagination<GetAllUsersDto>>
    {
        public int Limit { get; set; }
        public int Page { get; set; }
    }
    public sealed class Handler(IMapper mapper,IUnitOfWork unitOfWork) : IRequestHandler<GetAllCommand, ResponseModelPagination<GetAllUsersDto>>
    {
        public async Task<ResponseModelPagination<GetAllUsersDto>> Handle(GetAllCommand request, CancellationToken cancellationToken)
        {
            var users =await unitOfWork.UserRepository.GetAll();

            var total = users.Count();

            users = users.Skip((request.Page-1)*(request.Limit)).ToList();  

            var itemsMapped = new List<GetAllUsersDto
                >();

            foreach (var user in users) { 
            var data = mapper.Map<GetAllUsersDto>(user);    
            itemsMapped.Add(data);  
            }
            var responseModel = new Pagination<GetAllUsersDto>
            {
                Data = itemsMapped,
                TotalCount = total
            };
            return new ResponseModelPagination<GetAllUsersDto> {        Data = responseModel ,IsSuccess=true};
        }
    }
}
