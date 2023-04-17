using RSApp.Core.Domain.Core;

namespace RSApp.Core.Domain.Entities;

public class Image : BaseEntity {
  public int PropertyId { get; set; }
  public string ImagePath { get; set; } = null!;
  public Property Property { get; set; } = null!;
}
