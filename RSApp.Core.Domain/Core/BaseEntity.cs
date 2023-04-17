namespace RSApp.Core.Domain.Core;

public abstract class BaseEntity {
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime LastModifiedAt { get; set; }
}