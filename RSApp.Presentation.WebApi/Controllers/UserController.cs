using Microsoft.AspNetCore.Mvc;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;

namespace RSApp.Presentation.WebApi.Controllers;

public class UserController : BaseApiController {
  private readonly IAccountService _accountService;

  public UserController(IAccountService accountService) => _accountService = accountService;

  [HttpPost("authenticate")]
  public async Task<IActionResult> AuthenticateAsync([FromQuery] AuthenticationRequest request) => Ok(await _accountService.AuthenticateAsync(request, true));

}
