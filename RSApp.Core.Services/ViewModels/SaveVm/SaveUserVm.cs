using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RSApp.Core.Services.ViewModels.SaveVm;

public class SaveUserVm : SavePersonVm {
  [Required(ErrorMessage = "Username is required")]
  [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
  [MaxLength(20, ErrorMessage = "Username must be at most 20 characters long")]
  [DataType(DataType.Text)]
  public string UserName { get; set; } = null!;
  [Required(ErrorMessage = "Password is required")]
  [DataType(DataType.Password)]
  public string Password { get; set; } = null!;
  [Required(ErrorMessage = "Confirm Password is required")]
  [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
  [DataType(DataType.Password)]
  public string ConfirmPassword { get; set; } = null!;
  [Required(ErrorMessage = "Confirm Password is required")]
  public int Role { get; set; }
  [DataType(DataType.Upload)]
  public IFormFile? ImageFile { get; set; } = null!;
  public string? Image { get; set; } = null!;
}
