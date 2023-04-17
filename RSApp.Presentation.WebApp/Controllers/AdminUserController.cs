using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.ViewModels.SaveVm;
using RSApp.Presentation.WebApp.helpers;
using RSApp.Presentation.WebApp.Middleware;

namespace RSApp.Presentation.WebApp.Controllers;

public class AdminUserController : Controller {
  private readonly IUserService _userService;

  public AdminUserController(IUserService userService) {
    _userService = userService;
  }

  [Authorize(Roles = "Admin")]
  public IActionResult Index() => View();
  public IActionResult Create() => View(new SaveUserVm());

  [HttpPost]
  public async Task<IActionResult> Create(SaveUserVm model) {
    if (!ModelState.IsValid)
      return View(model);

    var origin = Request.Headers["origin"];

    RegisterResponse response = await _userService.RegisterAsync(model, origin);
    if (response.HasError) {
      model.HasError = response.HasError;
      model.Error = response.Error;
      return View(model);
    }

    if (response.UserId != null) {
      var user = await _userService.GetEntity(response.UserId);
      user.Image = ManageFile.Upload(model.ImageFile, response.UserId);
      await _userService.UpdateUserAsync(user);
    }

    return RedirectToRoute(new { controller = "User", action = "Index" });
  }

  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> ChangeStatus(string id) {
    var userIsVerify = await _userService.GetById(id);

    await _userService.ChangeStatus(userIsVerify.Id);

    return View("Index");
  }

  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Edit(string id) => View(await _userService.GetEntity(id));

  [Authorize(Roles = "Admin")]
  [ServiceFilter(typeof(SaveAuthorize))]
  [HttpPost]
  public async Task<IActionResult> Edit(SaveUserVm model) {
    if (!ModelState.IsValid)
      return View(model);

    RegisterResponse response = await _userService.UpdateUserAsync(model);
    if (response.HasError) {
      model.HasError = response.HasError;
      model.Error = response.Error;
      return View(model);
    }
    return RedirectToRoute(new { controller = "User", action = "Index" });
  }


}