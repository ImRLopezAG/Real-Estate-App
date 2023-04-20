using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Presentation.WebApp.Controllers;
[Authorize(Roles = "Admin")]
public class SaleController : Controller {
  private readonly ISaleService _saleService;

  public SaleController(ISaleService saleService) {
    _saleService = saleService;
  }
  public IActionResult Index() {
    return View();
  }

  public IActionResult Create() => View(new SaveSaleVm());

  [HttpPost]
  public async Task<IActionResult> Create(SaveSaleVm model) {
    if (!ModelState.IsValid)
      return View(model);
    try {
      await _saleService.Create(model);
      return RedirectToAction("Index");
    } catch (Exception ex) {
      model.HasError = true;
      model.ErrorMessage = ex.Message;
      return View(model);
    }
  }

  public async Task<IActionResult> Edit(int id) {
    var model = await _saleService.GetEntity(id);
    return View(model);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(SaveSaleVm model) {
    if (!ModelState.IsValid)
      return View(model);
    try {
      await _saleService.Edit(model);
      return RedirectToAction("Index");
    } catch (Exception ex) {
      model.HasError = true;
      model.ErrorMessage = ex.Message;
      return View(model);
    }
  }

  [HttpPost]
  public async Task<IActionResult> Delete(int id) {
    var model = await _saleService.GetEntity(id);
    if (model != null) {
      try {
        await _saleService.Delete(id);
        return RedirectToAction("Index");
      } catch (Exception ex) {
        model.HasError = true;
        model.ErrorMessage = ex.Message;
      }
    }
    return RedirectToAction("Index");
  }
}
