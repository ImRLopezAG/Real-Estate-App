using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Presentation.WebApp.Controllers;

public class UpgradeController: Controller
{
  private readonly IUpgradeService _upgradeService;

  public UpgradeController(IUpgradeService upgradeService) {
    _upgradeService = upgradeService;
  }
  public IActionResult Index() {
    return View();
  }

  public IActionResult Create() => View(new SaveUpgradeVm());

  [HttpPost]
  public async Task<IActionResult> Create(SaveUpgradeVm model) {
    if (!ModelState.IsValid)
      return View(model);
    try {
      await _upgradeService.Create(model);
      return RedirectToAction("Index");
    } catch (Exception ex) {
      model.HasError = true;
      model.ErrorMessage = ex.Message;
      return View(model);
    }
  }

  public async Task<IActionResult> Edit(int id) {
    var model = await _upgradeService.GetEntity(id);
    return View(model);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(SaveUpgradeVm model) {
    if (!ModelState.IsValid)
      return View(model);
    try {
      await _upgradeService.Edit(model);
      return RedirectToAction("Index");
    } catch (Exception ex) {
      model.HasError = true;
      model.ErrorMessage = ex.Message;
      return View(model);
    }
  }

  public async Task<IActionResult> Delete(int id) {
    var model = await _upgradeService.GetEntity(id);
    if (model != null) {
      try {
        await _upgradeService.Delete(id);
        return RedirectToAction("Index");
      } catch (Exception ex) {
        model.HasError = true;
        model.ErrorMessage = ex.Message;
      }
    }
    return RedirectToAction("Index");
  }
}
