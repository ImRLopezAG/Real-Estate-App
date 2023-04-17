using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class OwnPropertiesViewComponent: ViewComponent
{
  private readonly IPropertyService _propertyService;

  public OwnPropertiesViewComponent(IPropertyService propertyService)
  {
    _propertyService = propertyService;
  }

  public async Task<IViewComponentResult> InvokeAsync()
  {
    var properties = await _propertyService.GetOwnProperties();
    return View(properties);
  }
}
