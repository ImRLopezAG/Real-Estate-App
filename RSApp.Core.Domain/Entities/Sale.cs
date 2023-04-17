using RSApp.Core.Domain.Core;

namespace RSApp.Core.Domain.Entities;

public class Sale : BaseProperty {
  public ICollection<Property> Properties { get; set; } = null!;
}
