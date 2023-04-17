using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.ViewModels.SaveVm;
using RSApp.Core.Services.ViewModels.User;

namespace RSApp.Core.Services.Contracts;

public interface IUserService {
  Task<string> ConfirmEmailAsync(string userId, string token);
  Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordVm vm, string origin);
  Task<AuthenticationResponse> LoginAsync(LoginVm vm);
  Task<RegisterResponse> RegisterAsync(SaveUserVm vm, string origin);
  Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordVm vm);
  Task SignOutAsync();
  Task<RegisterResponse> UpdateUserAsync(SaveUserVm vm);
  Task ChangeStatus(string id);
  Task<SaveUserVm> GetEntity(string id);
  Task<IEnumerable<AccountDto>> GetAll();
  Task<AccountDto> GetById(string id);
}

