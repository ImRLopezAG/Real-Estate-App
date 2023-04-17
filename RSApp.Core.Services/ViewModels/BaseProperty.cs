using RSApp.Core.Services.Core.Models;

namespace RSApp.Core.Services.ViewModels;

public class BaseProperty : BaseVm {
  public string Name { get; set; } = null!;
  public string Description { get; set; } = null!;
}
