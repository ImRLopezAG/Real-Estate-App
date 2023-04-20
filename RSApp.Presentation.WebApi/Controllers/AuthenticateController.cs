using Microsoft.AspNetCore.Mvc;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Presentation.WebApi.Controllers;

public class AuthenticateController : BaseApiController {
  private readonly IAccountService _accountService;

  public AuthenticateController(IAccountService accountService) => _accountService = accountService;

  [HttpPost("Authenticate")]
  [SwaggerOperation(
        summary: "User authentication",
        description: "Get the parameters to authenticate a user"
  )]
  
  [SwaggerResponse(200, "User authenticated", typeof(AuthenticationResponse))]
  public async Task<IActionResult> AuthenticateAsync([FromQuery] AuthenticationRequest request) => Ok(await _accountService.AuthenticateAsync(request, true));

}
