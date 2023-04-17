using RSApp.Core.Services.Core.Models;

namespace RSApp.Core.Services.ViewModels.SaveVm;

public class SaveUpgradeVm : BaseVm {
  public string Name { get; set; } = null!;
  public string Description { get; set; } = null!;
}
