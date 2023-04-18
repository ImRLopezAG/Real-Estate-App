using System.Text.Json.Serialization;

namespace RSApp.Core.Services.Core.Models;

public class BaseVm : Base {
  [JsonIgnore]
  public bool HasError { get; set; }
  [JsonIgnore]
  public string? ErrorMessage { get; set; } = null!;
}
