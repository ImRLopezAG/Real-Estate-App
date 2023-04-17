using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSApp.Core.Services.Dtos.Favorite;

public class FavoriteDto
{
  public int PropertyId { get; set; }
  public string UserId { get; set; } = null!;
}
