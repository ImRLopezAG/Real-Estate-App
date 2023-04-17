namespace RSApp.Core.Services.Dtos.Property;

public class SavePropertyDto {
  public string Code { get; set; } = null!;
  public double Price { get; set; }
  public double Area { get; set; }
  public int Rooms { get; set; }
  public int Bathrooms { get; set; }
  public string Description { get; set; } = null!;
  public string Agent { get; set; } = null!;
  public string Portrait { get; set; } = null!;
  public int TypeId { get; set; }
  public int SaleId { get; set; }
}
