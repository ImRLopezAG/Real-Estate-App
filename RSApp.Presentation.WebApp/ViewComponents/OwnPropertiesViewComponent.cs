using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.Helpers;
using RSApp.Core.Services.Services;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class OwnPropertiesViewComponent : ViewComponent {
  private readonly IPropertyService _propertyService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse? _currentUser;

  public OwnPropertiesViewComponent(IPropertyService propertyService, IHttpContextAccessor httpContextAccessor) {
    _propertyService = propertyService;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
  }

  public async Task<IViewComponentResult> InvokeAsync() {
    var properties = await _propertyService.GetAll().ContinueWith(x => x.Result.Where(p => p.Agent == _currentUser.Id));
    return View(properties);
  }
}
