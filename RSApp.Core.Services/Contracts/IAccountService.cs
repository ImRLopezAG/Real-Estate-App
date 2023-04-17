using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Services.Contracts;

public interface IAccountService {
  Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, bool fromApi = false);
  Task<string> ConfirmAccountAsync(string userId, string token);
  Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
  Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
  Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
  Task SignOutAsync();
  Task<RegisterResponse> UpdateUserAsync(RegisterRequest request);
  Task<SaveUserVm> GetEntity(string id);
  Task ChangeStatus(string id);
  Task<IEnumerable<AccountDto>> GetAll();
  Task<AccountDto> GetById(string id);
}
