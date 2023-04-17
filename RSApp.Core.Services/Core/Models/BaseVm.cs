namespace RSApp.Core.Services.Core.Models;

public class BaseVm : Base {
  public bool HasError { get; set; }
  public string? ErrorMessage { get; set; } = null!;
}
