using Application.CQRS.Users.DTOs;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repository.Common;
using Repository.Repositories;

namespace Application.CQRS.Users.Handlers;

public class Register
{

    public record struct RegisterCommand : IRequest<ResponseModel
        <RegisterUserDto>>
    {
        public RegisterCommand()
        { }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Username { get; set; }
        public string? FatherName { get; set; }
        public required string Email { get; set; } //email olmasin validasiya edin
        public required string PasswordHash { get; set; }
        public string? Address { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) :
IRequestHandler<RegisterCommand, ResponseModel<RegisterUserDto>>
    {
        private readonly IMapper _mapper = mapper;

        private readonly IUnitOfWork _unitOfWork = unitOfWork;





        public async Task<ResponseModel<RegisterUserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            int? imageId = null;
            if (request.ProfileImage != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.ProfileImage.FileName)}";
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                var filePath = Path.Combine(folderPath, fileName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProfileImage.CopyToAsync(stream);
                }

                // Database-ə Image əlavə et
                var image = new Image
                {
                    FileName = fileName,
                    Location = $"/images/{fileName}",
                    CreatedAt = DateTime.UtcNow
                };

                imageId = await _unitOfWork.ImageRepository.AddImage(image);
            }

            // Yeni user yarat
            var user = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = PasswordHasher.ComputeStringToSha256Hash(request.PasswordHash), // Şifrələmə
                Address = request.Address,
                ImageId = imageId // Şəkili user-ə bağladıq
            };

            // User-i bazaya əlavə et
            await _unitOfWork.UserRepository.RegisterUser(user);
            await _unitOfWork.SaveChangesAsync();

            // Dönən məlumat
            var result = _mapper.Map<RegisterUserDto>(user);
            return new ResponseModel<RegisterUserDto>
            {
                Data = result,
                IsSuccess = true
            };



        }
    }

}
