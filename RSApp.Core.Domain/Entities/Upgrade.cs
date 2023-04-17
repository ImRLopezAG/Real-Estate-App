using RSApp.Core.Domain.Core;

namespace RSApp.Core.Domain.Entities;

public class Upgrade : BaseProperty {
  public ICollection<PropertyUpgrade> PropertyUpgrades { get; set; } = null!;
}