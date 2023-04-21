using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class PropertyListViewComponent : ViewComponent
{
  private readonly IPropertyService _propertyService;
  private readonly IFavoriteService _favoriteService;

  public PropertyListViewComponent(IPropertyService propertyService, IFavoriteService favoriteService)
  {
    _propertyService = propertyService;
    _favoriteService = favoriteService;
  }

  public async Task<IViewComponentResult> InvokeAsync(bool isFavorite = false){
    var favorites = await _favoriteService.GetAll();
    var properties = await _propertyService.GetAll();
    if (isFavorite)
      properties = properties.Where(p => favorites.Any(f => f.PropertyId == p.Id)).ToList();
    return View(properties);
  }
}
