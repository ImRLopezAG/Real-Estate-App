using RSApp.Core.Services.Core.Models;

namespace RSApp.Core.Services.ViewModels;

public class FavoriteVm : BaseVm {
  public int PropertyId { get; set; }
  public string UserId { get; set; } = null!;
}
