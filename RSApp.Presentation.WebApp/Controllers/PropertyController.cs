using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.Helpers;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Presentation.WebApp.Controllers;
[Authorize(Policy = "AgentOrClient")]
public class PropertyController : Controller {
  private readonly IPropertyService _propertyService;
  private readonly IFavoriteService _favoriteService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse _currentUser;

  public PropertyController(IPropertyService propertyService, IFavoriteService favoriteService, IHttpContextAccessor httpContextAccessor) {
    _propertyService = propertyService;
    _favoriteService = favoriteService;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }
  public IActionResult Index() => View();

  [HttpPost]
  public async Task<IActionResult> AddToFavorite(int propertyId) {
    var url = Request.Headers["Referer"].ToString();
    var entity = await _propertyService.GetEntity(propertyId);
    if (entity != null) {
      var favorite = new SaveFavoriteVm() {
        PropertyId = propertyId,
        UserId = _currentUser.Id
      };
      await _favoriteService.Create(favorite);
    }
    return RedirectToRoute(new { controller = url.Contains("Property") ? "Property" : "Home", action = "Index" });
  }

  [HttpPost]
  public async Task<IActionResult> DeleteFromFavorite(int propertyId) {
    var url = Request.Headers["Referer"].ToString();

    var entity = await _favoriteService.GetByPropAndUser(propertyId, _currentUser.Id);
    if (entity != null) {
      await _favoriteService.Delete(entity.Id);
    }
    return RedirectToRoute(new { controller = url.Contains("Property") ? "Property" : "Home", action = "Index" });
  }

  public async Task<IActionResult> Delete(int id) {
    var url = Request.Headers["Referer"].ToString();
    await _propertyService.Delete(id);
    return RedirectToRoute(new { controller = url.Contains("Property") ? "Property" : "Home", action = "Index" });
  }
}
