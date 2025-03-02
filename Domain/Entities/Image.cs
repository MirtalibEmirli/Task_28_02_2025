using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Image
{
    public int Id { get; set; } 
    public string FileName { get; set; } = null!;   
    public string Location { get; set; } = null!; // "/wwwroot/images/GUID.jpg"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

