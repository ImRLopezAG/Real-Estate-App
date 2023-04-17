using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class FavoriteListViewComponent: ViewComponent
{
  private readonly IPropertyService _propertyService;

  public FavoriteListViewComponent(IPropertyService propertyService)
  {
    _propertyService = propertyService;
  }

  public async Task<IViewComponentResult> InvokeAsync()
  {
    var favorites = await _propertyService.GetFavorites();
    return View(favorites);
  }
}
