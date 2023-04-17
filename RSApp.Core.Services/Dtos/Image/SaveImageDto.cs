using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSApp.Core.Services.Dtos.Image;

public class SaveImageDto
{
  public int PropertyId { get; set; }
  public string ImagePath { get; set; } = null!;
}
