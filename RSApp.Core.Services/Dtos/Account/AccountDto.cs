using System.Text.Json.Serialization;

namespace RSApp.Core.Services.Dtos.Account;

public class AccountDto {
    public string Id { get; set; } = null!;
    [JsonIgnore]
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonIgnore]
    public string FullName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    [JsonIgnore]
    public string DNI { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
    public int Products { get; set; }
    [JsonIgnore]
    public string Role { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    [JsonIgnore]
    public string Image { get; set; } = null!;
}
