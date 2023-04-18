namespace RSApp.Core.Application.Features.Core;

/// <summary>
/// Basic response for all entities
/// </summary>
public class BasicResponse {
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; } = null!;
}
