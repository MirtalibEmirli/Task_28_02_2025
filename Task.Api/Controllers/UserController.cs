using Application.CQRS.Users.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Common;
using static Application.CQRS.Users.Handlers.GetUserById;
using static Application.CQRS.Users.Handlers.Register;

namespace Task.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController(ISender sender) : ControllerBase
{
    private readonly ISender _sender=sender;

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromForm] RegisterCommand registerCommand)
    {
        Console.WriteLine(registerCommand.BirthDate);
        return Ok( await _sender.Send(registerCommand));
    }




    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var request = new GetUserByIdCommand() { UserId=id};
        return Ok( await _sender.Send(request));    
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsers.GetAllCommand request ) {
    
    return Ok( await _sender.Send(request));    
    } 
    
 
}
