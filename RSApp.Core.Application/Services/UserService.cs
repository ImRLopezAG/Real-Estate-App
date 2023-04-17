using AutoMapper;
using Microsoft.AspNetCore.Http;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.ViewModels.SaveVm;
using RSApp.Core.Services.ViewModels.User;

namespace RSApp.Core.Application.Services;

public class UserService : IUserService {
  private readonly IAccountService _accountService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IEmailService _emailService;
  private readonly IMapper _mapper;

  public UserService(IAccountService accountService, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IMapper mapper) {
    _accountService = accountService;
    _httpContextAccessor = httpContextAccessor;
    _emailService = emailService;
    _mapper = mapper;
  }

  public async Task<AuthenticationResponse> LoginAsync(LoginVm model) {
    AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(model);
    AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
    return userResponse;
  }
  public async Task SignOutAsync() {
    await _accountService.SignOutAsync();
  }

  public async Task<RegisterResponse> RegisterAsync(SaveUserVm model, string origin) {
    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(model);
    var registerResponse = await _accountService.RegisterUserAsync(registerRequest, origin);
    if (registerResponse.HasError)
      return registerResponse;
    return registerResponse;
  }

  public async Task<string> ConfirmEmailAsync(string userId, string token) {
    return await _accountService.ConfirmAccountAsync(userId, token);
  }

  public async Task<RegisterResponse> UpdateUserAsync(SaveUserVm vm) {
    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
    return await _accountService.UpdateUserAsync(registerRequest);
  }

  public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordVm model, string origin) {
    ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(model);
    return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
  }

  public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordVm model) {
    ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(model);
    return await _accountService.ResetPasswordAsync(resetRequest);
  }


  public async Task<IEnumerable<AccountDto>> GetAll() {
    var query = from user in await _accountService.GetAll()
                select user;
    return query;
  }

  public async Task<AccountDto> GetById(string id) => await _accountService.GetById(id);

  public async Task<SaveUserVm> GetEntity(string id) => await _accountService.GetEntity(id);

  public async Task ChangeStatus(string id) => await _accountService.ChangeStatus(id);

}
