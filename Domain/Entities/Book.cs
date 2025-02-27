using Domain.BaseEntities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities;

public  class Book:BaseEntity
{
    public  required string Author { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid? CoverPhoto { get; set; }
    public Guid UserId { get; set; }
    public bool? ShowOnFirstScreen { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Languages Language { get; set; } //enum
}
