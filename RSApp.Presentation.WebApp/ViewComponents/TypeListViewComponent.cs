using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class TypeListViewComponent : ViewComponent {
  private readonly IPropTypeService _typeService;

  public TypeListViewComponent(IPropTypeService typeService) {
    _typeService = typeService;
  }

  public async Task<IViewComponentResult> InvokeAsync() {
    var types = await _typeService.GetAll();
    return View(types);
  }

}
