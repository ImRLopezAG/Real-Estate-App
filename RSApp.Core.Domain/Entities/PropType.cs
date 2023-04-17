using RSApp.Core.Domain.Core;

namespace RSApp.Core.Domain.Entities;

public class PropType : BaseProperty {
  public ICollection<Property> Properties { get; set; } = null!;
}
