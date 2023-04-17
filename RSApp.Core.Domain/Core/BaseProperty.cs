namespace RSApp.Core.Domain.Core;

public class BaseProperty : BaseEntity {
  public string Name { get; set; } = null!;
  public string Description { get; set; } = null!;
}

