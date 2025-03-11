using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.DTOs;

public class GetAllUsersDto
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Username { get; set; }
    public string? FatherName { get; set; }
    public required string Email { get; set; } //email olmasin validasiya edin
    public required string? PasswordHash { get; set; }
    public string? Address { get; set; }
    public string? MobilePhone { get; set; } // "+994 le bashlamasin validasiya edin"
    public string? CardNumber { get; set; } // 16 reqemi validasiya edirsiniz

    public string? TableNumber { get; set; }
    public DateTime Birthdate { get; set; }

    public DateTime? DateOfEmployment { get; set; }
    public DateTime? DateOfDismissal { get; set; }
    public string? ImageUrl { get; set; }

}
