using System.ComponentModel.DataAnnotations;

namespace RSApp.Core.Services.ViewModels.User;

public class ForgotPasswordVm : ValidationVm {
  [Required(ErrorMessage = "The email is required")]
  [DataType(DataType.Text)]
  public string Email { get; set; } = null!;
}
