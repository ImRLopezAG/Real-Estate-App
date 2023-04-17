using RSApp.Core.Services.Core.Models;

namespace RSApp.Core.Services.ViewModels.SaveVm;

public class SaveFavoriteVm : BaseVm {
  public int PropertyId { get; set; }
  public string UserId { get; set; } = null!;
}
