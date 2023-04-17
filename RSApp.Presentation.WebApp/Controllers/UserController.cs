using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.Helpers;
using RSApp.Core.Services.ViewModels.User;
using RSApp.Presentation.WebApp.Middleware;

namespace RSApp.Presentation.WebApp.Controllers;

public class UserController : Controller {
  private readonly IUserService _userService;

  public UserController(IUserService userService) {
    _userService = userService;
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  public IActionResult Index() {
    return View(new LoginVm());
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  [HttpPost]
  public async Task<IActionResult> Index(LoginVm model) {
    if (!ModelState.IsValid) {
      return View(model);
    }
    AuthenticationResponse user = await _userService.LoginAsync(model);
    if (user != null && user.HasError != true) {
      HttpContext.Session.Set<AuthenticationResponse>("user", user);
      return RedirectToRoute(new { controller = "Home", action = "Index" });
    } else {
      model.HasError = user.HasError;
      model.Error = user.Error;
      return View(model);
    }
  }
  public async Task<IActionResult> LogOut() {
    await _userService.SignOutAsync();
    HttpContext.Session.Remove("user");
    HttpContext.Response.Cookies.Delete("token");
    HttpContext.Response.Cookies.Delete("Authorization");
    return RedirectToRoute(new { controller = "User", action = "Index" });
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  public async Task<IActionResult> ConfirmEmail(string userId, string token) {
    string response = await _userService.ConfirmEmailAsync(userId, token);
    return View("ConfirmEmail", response);
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  public IActionResult ForgotPassword() {
    return View(new ForgotPasswordVm());
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  [HttpPost]
  public async Task<IActionResult> ForgotPassword(ForgotPasswordVm vm) {
    if (!ModelState.IsValid) {
      return View(vm);
    }
    var origin = Request.Headers["origin"];
    ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);
    if (response.HasError) {
      vm.HasError = response.HasError;
      vm.Error = response.Error;
      return View(vm);
    }
    return RedirectToRoute(new { controller = "User", action = "Index" });
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  public IActionResult ResetPassword(string token) {
    return View(new ResetPasswordVm { Token = token });
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  [HttpPost]
  public async Task<IActionResult> ResetPassword(ResetPasswordVm vm) {
    if (!ModelState.IsValid) {
      return View(vm);
    }

    ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);
    if (response.HasError) {
      vm.HasError = response.HasError;
      vm.Error = response.Error;
      return View(vm);
    }
    return RedirectToRoute(new { controller = "User", action = "Index" });
  }

  public IActionResult AccessDenied() {
    return View();
  }
}
