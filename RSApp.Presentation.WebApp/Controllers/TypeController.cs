using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Presentation.WebApp.Controllers;
[Authorize(Roles = "Admin")]
public class TypeController : Controller {
  private readonly IPropTypeService _typeService;

  public TypeController(IPropTypeService typeService) {
    _typeService = typeService;
  }
  public IActionResult Index() {
    return View();
  }

  public IActionResult Create() => View(new SavePropTypeVm());

  [HttpPost]
  public async Task<IActionResult> Create(SavePropTypeVm model) {
    if (!ModelState.IsValid)
      return View(model);
    try {
      await _typeService.Create(model);
      return RedirectToAction("Index");
    } catch (Exception ex) {
      model.HasError = true;
      model.ErrorMessage = ex.Message;
      return View(model);
    }
  }

  public async Task<IActionResult> Edit(int id) {
    var model = await _typeService.GetEntity(id);
    return View(model);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(SavePropTypeVm model) {
    if (!ModelState.IsValid)
      return View(model);
    try {
      await _typeService.Edit(model);
      return RedirectToAction("Index");
    } catch (Exception ex) {
      model.HasError = true;
      model.ErrorMessage = ex.Message;
      return View(model);
    }
  }

  public async Task<IActionResult> Delete(int id) {
    var model = await _typeService.GetEntity(id);
    if (model != null) {
      try {
        await _typeService.Delete(id);
        return RedirectToAction("Index");
      } catch (Exception ex) {
        model.HasError = true;
        model.ErrorMessage = ex.Message;
      }
    }
    return RedirectToAction("Index");
  }
}