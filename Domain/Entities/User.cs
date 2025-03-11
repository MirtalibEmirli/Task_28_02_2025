using Domain.BaseEntities;
using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User:BaseEntity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Username { get; set; }
        public string? FatherName { get; set; }
        public required string Email { get; set; } //email olmasin validasiya edin
        public required string PasswordHash { get; set; }
        public string? Address { get; set; }
        public string? MobilePhone { get; set; } // "+994 le bashlamasin validasiya edin"
        public string? CardNumber { get; set; } // 16 reqemi validasiya edirsiniz

        public string? TableNumber { get; set; }
        public DateTime Birthdate { get; set; }

        public DateTime? DateOfEmployment { get; set; }
        public DateTime? DateOfDismissal { get; set; }

        public string? Note { get; set; }


        public int? ImageId { get; set; }  
        public string? ImageUrl{ get; set; }  
        public Image? Image { get; set; }  

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }  // Enum


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role UserType { get; set; } // Enum
    }
}
