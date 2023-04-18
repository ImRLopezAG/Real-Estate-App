using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Presentation.WebApi.Controllers;

public class UserController: BaseApiController
{
  private readonly IAccountService _accountService;

  public UserController(IAccountService accountService) => _accountService = accountService;

  [HttpPost("Register")]
  [SwaggerOperation(
    summary: "User registration",
    description: "Get the parameters to register a user"
  )]
  public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request){
    var origin = Request.Headers["origin"];
    return Ok(await _accountService.RegisterUserAsync(request, origin));
  }
}
