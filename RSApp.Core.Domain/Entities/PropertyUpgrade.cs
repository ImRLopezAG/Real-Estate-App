using RSApp.Core.Domain.Core;

namespace RSApp.Core.Domain.Entities;

public class PropertyUpgrade : BaseEntity {
  public int PropertyId { get; set; }
  public int UpgradeId { get; set; }
  public Upgrade Upgrade { get; set; } = null!;
  public Property Property { get; set; } = null!;

}
