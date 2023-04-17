using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class SaleListViewComponent : ViewComponent {
  private readonly ISaleService _saleService;

  public SaleListViewComponent(ISaleService saleService) {
    _saleService = saleService;
  }

  public async Task<IViewComponentResult> InvokeAsync() {
    var sales = await _saleService.GetAll();
    return View(sales);
  }
}
