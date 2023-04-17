namespace RSApp.Core.Services.Dtos.Account;
public class AuthenticationRequest {
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;
}

