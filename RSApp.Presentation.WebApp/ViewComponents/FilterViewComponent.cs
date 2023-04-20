using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class FilterViewComponent: ViewComponent
{
  private readonly IPropTypeService _propTypeService;

  public FilterViewComponent(IPropTypeService propTypeService)
  {
    _propTypeService = propTypeService;
  }
  public async Task<IViewComponentResult> InvokeAsync()
  {
    var propTypes = await _propTypeService.GetAll();
    return View(propTypes);
  }
}
