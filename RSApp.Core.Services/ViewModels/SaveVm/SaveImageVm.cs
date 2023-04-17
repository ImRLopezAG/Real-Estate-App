using RSApp.Core.Services.Core.Models;

namespace RSApp.Core.Services.ViewModels.SaveVm;

public class SaveImageVm : BaseVm {
  public int PropertyId { get; set; }
  public string ImagePath { get; set; } = null!;
}
