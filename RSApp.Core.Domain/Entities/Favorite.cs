using RSApp.Core.Domain.Core;

namespace RSApp.Core.Domain.Entities;

public class Favorite : BaseEntity {
  public int PropertyId { get; set; }
  public string UserId { get; set; } = null!;
  public Property Property { get; set; } = null!;
}
