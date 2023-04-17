namespace RSApp.Core.Application.Features.Core;

public class BasicResponse {
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; } = null!;
}
