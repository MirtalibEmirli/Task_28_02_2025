using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Common;
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
    
 
}
