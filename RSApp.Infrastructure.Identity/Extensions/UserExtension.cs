using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.ViewModels.SaveVm;
using RSApp.Infrastructure.Identity.Entities;

namespace RSApp.Infrastructure.Identity.Extensions;

public static class UserExtension {
  public static AccountDto ToAccountDto(this ApplicationUser user, string role) => new() {

    Id = user.Id,
    Email = user.Email,
    EmailConfirmed = user.EmailConfirmed,
    FirstName = user.FirstName,
    LastName = user.LastName,
    FullName = $"{user.FirstName} {user.LastName}",
    PhoneNumber = user.PhoneNumber,
    DNI = user.DNI,
    UserName = user.UserName,
    Role = role,
    Image = user.Image
  };

  public static SaveUserVm ToSaveVm(this ApplicationUser user, int role) => new() {
    Id = user.Id,
    Email = user.Email,
    FirstName = user.FirstName,
    LastName = user.LastName,
    PhoneNumber = user.PhoneNumber,
    DNI = user.DNI,
    UserName = user.UserName,
    Role = role,
    Image = user.Image
  };
}
