using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.ViewModels.SaveVm;
using RSApp.Presentation.WebApp.helpers;
using RSApp.Presentation.WebApp.Middleware;

namespace RSApp.Presentation.WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminUserController : Controller {
  private readonly IUserService _userService;

  public AdminUserController(IUserService userService) {
    _userService = userService;
  }
  public IActionResult Index() => View();

  public async Task<IActionResult> ChangeStatus(string id) {
    var userIsVerify = await _userService.GetById(id);

    await _userService.ChangeStatus(userIsVerify.Id);

    return View("Index");
  }

  public IActionResult Agents() => View();
  public IActionResult Clients() => View();
  public IActionResult Developers() => View();
  public IActionResult Admins() => View();

  public async Task<IActionResult> Edit(string id) => View(await _userService.GetEntity(id));

  [HttpPost]
  public async Task<IActionResult> Edit(SaveUserVm model) {
    if (!ModelState.IsValid)
      return View(model);

    try {
      var user = await _userService.GetEntity(model.Id);
      user.Image = ManageFile.Upload(model.ImageFile, model.Id, true, user.Image);
      await _userService.UpdateUserAsync(user);
      return RedirectToRoute(new { controller = "AdminUser", action = "Index" });
    } catch (Exception ex) {
      model.HasError = true;
      model.Error = ex.Message;
      return View(model);
    }
  }
}