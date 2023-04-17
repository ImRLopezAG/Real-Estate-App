using RSApp.Core.Domain.Core;

namespace RSApp.Core.Domain.Entities;

public class Property : BaseEntity {
  public string Code { get; set; } = null!;
  public double Price { get; set; }
  public double Area { get; set; }
  public int Rooms { get; set; }
  public int Bathrooms { get; set; }
  public string Description { get; set; } = null!;
  public string Agent { get; set; } = null!;
  public string? Portrait { get; set; } = null!;

  // navigation properties
  public int TypeId { get; set; }
  public int SaleId { get; set; }
  public PropType Type { get; set; } = null!;
  public Sale Sale { get; set; } = null!;
  public ICollection<Image> Images { get; set; } = null!;
  public ICollection<PropertyUpgrade> PropertyUpgrades { get; set; } = null!;
  public ICollection<Favorite> Favorites { get; set; } = null!;
}
