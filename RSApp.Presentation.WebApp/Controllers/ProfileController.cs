
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.Helpers;
using RSApp.Core.Services.ViewModels.SaveVm;
using RSApp.Presentation.WebApp.helpers;

namespace RSApp.Presentation.WebApp.Controllers;
[Authorize]
public class ProfileController: Controller
{
  private readonly IUserService _userService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse _currentUser;

  public ProfileController(IUserService userService, IHttpContextAccessor httpContextAccessor)
  {
    _userService = userService;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }

  
  public async Task<IActionResult> Index() => View(await _userService.GetEntity(_currentUser.Id));

  [HttpPost]
  public async Task<IActionResult> Index(SaveUserVm model) {
    if (!ModelState.IsValid)
      return View(model);
    try{
      var user = await _userService.GetEntity(_currentUser.Id);
      user.Image = ManageFile.Upload(model.ImageFile, _currentUser.Id, true, user.Image);
      await _userService.UpdateUserAsync(user);
      return RedirectToRoute(new { controller = "Profile", action = "Index" });
    }catch(Exception ex){
      model.HasError = true;
      model.Error = ex.Message;
      return View(model);
    }
  }


}
