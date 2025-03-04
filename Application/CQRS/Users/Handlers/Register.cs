using Application.CQRS.Users.DTOs;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repository.Common;
using System;
namespace Application.CQRS.Users.Handlers;

public class Register
{

    
    public class RegisterCommand : IRequest<ResponseModel
        <RegisterUserDto>>
    {

        public RegisterCommand()
        {

        }

        public RegisterCommand(string name, string surname, string username, string email, string passwordHash,
            string cardNumber, string mobilPhone, DateTime birthDate, string role, IFormFile profileImage, string gender)
        {
            Name = name;
            Surname = surname;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            CardNumber = cardNumber;
            MobilePhone = mobilPhone;
            BirthDate = birthDate;
            Role = role;
            ProfileImage = profileImage;
            Gender = gender;
        }

        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; } //email olmasin validasiya edin
        public required string PasswordHash { get; set; }
        public required string CardNumber { get; set; }
        public required string MobilePhone { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string Role { get; set; }
        public required IFormFile ProfileImage { get; set; }
        public required string Gender { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) :
IRequestHandler<RegisterCommand, ResponseModel<RegisterUserDto>>
    {
        private readonly IMapper _mapper = mapper;

        private readonly IUnitOfWork _unitOfWork = unitOfWork;





        public async Task<ResponseModel<RegisterUserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            //bu kod 3 hisseden ibaretdir birinci hisse budur ki => Siz ilk once ImageFile
            //yaradirsiz ve onu foldere yazirsiz.Yeni requestde gonderilen IFormFile tipi ile gelen
            //image i database e yazmamis qeyd edirsiz oz kompyuterinizde wwwroot => Static files
            //hissesine  ve artiq sekil sizin kompyuterde var .Bu prossess de sadedir.IFormFile kimi gelen file den isdifade ederek , fileExtension u alirsiz ve fayli yaraadirsiz (Guid ile).
            //Novbeti olaraq bize yardim eden Path.GetCurrentDirectory dir.Bu kodun isdenilen kompyuterde islemesini temin edir ve folder(wwwroot) yoxdursa yaradir.
            //Novbeti hisse ise bize gelen IFormFile i stream vasitesi ile kopyalamaqdir bizim filePath adli path e.Burada birinci hisse yekunlasir
   
         

           
 

            int imageId = 0;
            var iForm = request.ProfileImage;
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(iForm.FileName)}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
                await iForm.CopyToAsync(stream);

            //Indi biz Databasemizdeki Images  tablesine insert ederek bu sekilin melumatlarini =>
            //beli duz esiddiz sekili yox , melumatlarini database elave edirik 
            var img = new Image
            {
                CreatedAt = DateTime.Now,
                FileName = fileName,
                Location = filePath,
            };
            imageId = await _unitOfWork.ImageRepository.AddImage(img);
            //artiq user e bind edmek ucun lazim olan imageId propertysini de elde eddik
            //userimizi yaradib map edek ve elave edek database e
            //Sonuncu  hisse ise En yuxarida yaradilan imageId den isdifade eederek  insert edmek meqamidir.
            //userimizi map edirik ve database e elave edib , responseModeli geri qaytaririq

            var user = _mapper.Map<User>(request);
            user.PasswordHash = PasswordHasher.ComputeStringToSha256Hash(request.PasswordHash);
            user.ImageId = imageId;
            user.CreatedBy = 1;
            await _unitOfWork.UserRepository.RegisterUser(user);
            var result = _mapper.Map<RegisterUserDto>(user);
            return  new ResponseModel<RegisterUserDto> { Data = result ,IsSuccess=true};

            //Burada esas diqqet edmeli oldugunuz meqamalar :
            //1.Enum isdifade ederek RequestBody ,Entity ve Database elaqesi qurmaq
            //2.DateTime isdifade ederek RequestBody ,Entity ve Database elaqesi qurmaq.
            //Kenardan sade gorunsede bu hecde asan bir is deyil .Front terefde DateTimePicker isledirikse bu
            //rahat olacaqdir ,cunki DateTime ile uygundur.Databasede DateTime2 formati saxlamaq daha meqsede uygundur.
            //Amma DateTimePicker isdifade edmirikse burda artiq filtrasiya typesafety ise dusur.Biz artiq string kimi qebul edmeliyik bu DateTime i ve front a qebul eddiyimiz formati verib onun validasiya
            //olunmasini teleb edmeliyik ki , user bunu gonderende nece gondereceyni bilsi ex : MM/dd/yyyy ,
            //dd/MM/yyyy  , yyyy/MM/dd ve ya  yyyy/dd/MM . Bunlardan birini secim edib buna uygun mapping body yazmaliyiq ki map ederken konfliktler yaranmasin ve duzgun formatda datetime create edib database e add edek .
            //3.Bu hissede Meqalemizin main sebebi olan ImageServer idi .ImageServer menim arasdirmama gore sizin validasyalarinizdan asili olaraq deyise biler.
            //Yeni siz eger elave olaraq bu sekili ayri bir yerde isdifade edmeyeceksizse bunu Dal.SqlServer yeni 
            //Crud yazdiqiniz repositorylere qeder yazmaq kifayetdir.Applicationda elave mentiq ,CQRS lazim deyil.
            //Yeni Image menim var olan Entity classimda property olaraq isdifade olunubsa bu zaman bele yazmaq olar.
            //Buda crud ucun kifayetdir .Amma elave olaraq Image Entity classini basqa yerdede isdifade edirsinizse yeni size , endpoint kimi lazimdirsa bu zaman artiq Kontroller seviyyesine qaldira bilersiniz.Buda sizin goreceyiniz isden asilidir.Sekilleri saxlamaqa geldikde ise heleki deployda olmayan bir proyekt olaraq wwwroot folderi ile kifayet edir saxlamaq.Amma dusunuremki bunu dahada boyuddukde memory use arta biler ona gore her hansisa bir bulud texnologiyasi size destek ola biler.
            //Oxuduqunuz ucun tesekkur!









        }
    }

}


//ASP.NET Core API-də Enum, DateTime və Şəkil Yükləmə: Düzgün Yanaşma və Best Practices
//Bu yazıda ASP.NET Core API-də Enum-ların düzgün istifadəsi, DateTime formatlarının idarə olunması və şəkillərin yüklənməsi ilə bağlı əsas məqamları izah edəcəyəm.Layihənin tələblərinə uyğun olaraq, bu proseslərdə ən optimal həll yollarını tətbiq edəcəyik.