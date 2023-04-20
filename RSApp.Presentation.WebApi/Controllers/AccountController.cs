using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Presentation.WebApi.Controllers;

public class AccountController: BaseApiController
{
  private readonly IAccountService _accountService;

  public AccountController(IAccountService accountService) => _accountService = accountService;

  [HttpPost("RegisterAdmin")]
  [Consumes(MediaTypeNames.Application.Json)]
  [SwaggerOperation(
    summary: "User registration",
    description: "Get the parameters to register a user"
  )]
    public async Task<IActionResult> RegisterAdmin([FromQuery] RegisterRequest request){
    var origin = Request.Headers["origin"];
    request.Role = 1;
    return Ok(await _accountService.RegisterUserAsync(request, origin));
  }
  [HttpPost("RegisterDeveloper")]
  [Consumes(MediaTypeNames.Application.Json)]
  [SwaggerOperation(
    summary: "User registration",
    description: "Get the parameters to register a user"
  )]
  public async Task<IActionResult> RegisterDeveloper([FromQuery] RegisterRequest request){
    var origin = Request.Headers["origin"];
    request.Role = 2;
    return Ok(await _accountService.RegisterUserAsync(request, origin));
  }
}
