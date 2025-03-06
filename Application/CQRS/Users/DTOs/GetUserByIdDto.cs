using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Application.CQRS.Users.DTOs;

public record struct GetUserByIdDto
{
    public GetUserByIdDto(string name, string surname, string username, string email, string passwordHash, string cardNumber, string mobilePhone, DateTime birthDate, string role, IFormFile profileImage, string gender, string? imageUrl)
    {
        Name = name;
        Surname = surname;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        CardNumber = cardNumber;
        MobilePhone = mobilePhone;
        BirthDate = birthDate;
        Role = role;
        ProfileImage = profileImage;
        Gender = gender;
        ImageUrl = imageUrl;
    }
    public GetUserByIdDto()
    {
        
    }

    public required string Name { get; set; }

    public required string Surname { get; set; }

    public required string Username { get; set; }

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public required string CardNumber { get; set; }

    public required string MobilePhone { get; set; }

    public required DateTime BirthDate { get; set; }
    public required string Role { get; set; }

    public required IFormFile ProfileImage { get; set; }

    public required string Gender { get; set; }

    public string? ImageUrl { get; set; }

}
