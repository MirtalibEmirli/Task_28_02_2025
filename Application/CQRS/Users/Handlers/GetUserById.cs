using Application.CQRS.Users.DTOs;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

public class GetUserById
{

    public record struct GetUserByIdCommand:IRequest<ResponseModel<GetUserByIdDto>>
    {
        public GetUserByIdCommand()
        {
                
        }
        public GetUserByIdCommand(int id )
        {
            this.UserId = id;   
        }
        public required int UserId
        {
            get; set;
        }
    }


    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserByIdCommand,
ResponseModel<GetUserByIdDto>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseModel<GetUserByIdDto>> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {

            var user =await  _unitOfWork.UserRepository.GetById(request.UserId);
            if (user == null) throw new BadRequestException($"There is no user with provided id {request.UserId}");


            var responseModel = _mapper.Map<GetUserByIdDto>(user);
            return    new ResponseModel<GetUserByIdDto> { Data = responseModel, IsSuccess = true };


        }
    }
}
