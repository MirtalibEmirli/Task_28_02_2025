using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.DTOs;

public class RegisterUserDto
{
    public required string  Name { get; set; }
    public int Id { get; set; }
    public int ImageId { get; set; }
}
