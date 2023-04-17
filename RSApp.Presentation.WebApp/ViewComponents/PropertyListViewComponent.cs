using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class PropertyListViewComponent : ViewComponent {
  private readonly IPropertyService _propertyService;

  public PropertyListViewComponent(IPropertyService propertyService) {
    _propertyService = propertyService;
  }

  public async Task<IViewComponentResult> InvokeAsync() {
    var properties = await _propertyService.GetAll();
    return View(properties);
  }
}
