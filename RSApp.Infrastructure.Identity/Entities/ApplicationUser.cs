using Microsoft.AspNetCore.Identity;

namespace RSApp.Infrastructure.Identity.Entities;
public class ApplicationUser : IdentityUser {
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string DNI { get; set; } = null!;
  public string? Image { get; set; } = null!;
}
