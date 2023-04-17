using RSApp.Core.Services.Core.Models;

namespace RSApp.Core.Services.ViewModels;

public class ImageVm : BaseVm {
  public int PropertyId { get; set; }
  public string ImagePath { get; set; } = null!;
}
