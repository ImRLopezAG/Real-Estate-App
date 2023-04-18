using System.Text.Json.Serialization;

namespace RSApp.Core.Services.Core.Models;

public class Base {
  public int Id { get; set; }
  [JsonIgnore]
  public DateTime CreatedAt { get; set; }
  [JsonIgnore]
  public DateTime LastModifiedAt { get; set; }

}
