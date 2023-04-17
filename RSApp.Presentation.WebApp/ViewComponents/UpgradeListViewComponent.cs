using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class UpgradeListViewComponent : ViewComponent
{
  private readonly IUpgradeService _upgradeService;

  public UpgradeListViewComponent(IUpgradeService upgradeService)
  {
    _upgradeService = upgradeService;
  }

  public async Task<IViewComponentResult> InvokeAsync()
  {
    var upgrades = await _upgradeService.GetAll();
    return View(upgrades);
  }
}
