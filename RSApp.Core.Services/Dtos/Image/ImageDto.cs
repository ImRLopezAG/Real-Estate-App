using RSApp.Core.Services.Core.Models;

namespace RSApp.Core.Services.Dtos.Image;

public class ImageDto : Base {
  public int PropertyId { get; set; }
  public string ImagePath { get; set; } = null!;
}
