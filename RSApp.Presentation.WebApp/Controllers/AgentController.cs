using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.Helpers;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels.SaveVm;
using RSApp.Presentation.WebApp.helpers;

namespace RSApp.Presentation.WebApp.Controllers;

[Authorize(Roles = "Agent")]
public class AgentController : Controller
{
  private readonly IPropertyService _propertyService;
  private readonly IPropTypeService _propTypeService;
  private readonly ISaleService _saleService;
  private readonly IImageService _imageService;
  private readonly IUpgradeService _upgradeService;
  private readonly IUserService _userService;
  private readonly IPropUpgradeService _propUpgradeService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse? _currentUser;

  public AgentController(IPropertyService propertyService, IPropTypeService propTypeService, ISaleService saleService, IHttpContextAccessor httpContextAccessor, IImageService imageService, IUpgradeService upgradeService, IPropUpgradeService propUpgradeService, IUserService userService)
  {
    _propertyService = propertyService;
    _propTypeService = propTypeService;
    _saleService = saleService;
    _httpContextAccessor = httpContextAccessor;
    _imageService = imageService;
    _upgradeService = upgradeService;
    _propUpgradeService = propUpgradeService;
    _userService = userService;
    _currentUser = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
  }
  public IActionResult Index() => View();
  public IActionResult OwnProperty() => View();
  public async Task<IActionResult> Create() => View(new SavePropertyVm()
  {
    Types = await _propTypeService.GetAll(),
    Sales = await _saleService.GetAll(),
    Upgrades = await _upgradeService.GetAll()
  });

  [HttpPost]
  public async Task<IActionResult> Create(SavePropertyVm model)
  {
    if (!ModelState.IsValid)
      return View(await Error(model));

    model.Agent = _currentUser.Id;
    model.Code = Guid.NewGuid().ToString()[..8].Replace("-", "").ToUpper();

    var created = await _propertyService.Create(model);
    if (created.Id != 0)
    {
      created.Portrait = ManageFile.UploadProperty(model.ImageFile, _currentUser.Id, created.Id);

      await _propertyService.Edit(created);
      if (model.ImageFiles != null)
      {
        foreach (var image in model.ImageFiles)
        {
          var img = new SaveImageVm()
          {
            PropertyId = created.Id,
            ImagePath = ManageFile.UploadPropertyImages(image, _currentUser.Id, created.Id)
          };
          await _imageService.Create(img);
        }
      }
    }

    var upgrades = await _upgradeService.GetAll();
    var validateUpgrades = upgrades.Where(x => model.UpgradeId.Contains(x.Id)).ToList();

    List<SavePropUpgradeVm> propertyUpgrades = new();

    foreach (var upgrade in validateUpgrades)
    {
      propertyUpgrades.Add(new SavePropUpgradeVm()
      {
        PropertyId = created.Id,
        UpgradeId = upgrade.Id,
      });
    }

    await _propUpgradeService.SaveRange(propertyUpgrades);

    return RedirectToAction("OwnProperty");
  }

  public async Task<IActionResult> Edit(int id)
  {
    var property = await _propertyService.GetEntity(id);
    property.Sales = await _saleService.GetAll();
    property.Types = await _propTypeService.GetAll();
    property.Upgrades = await _upgradeService.GetAll();
    return View(property);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(SavePropertyVm model)
  {
    if (!ModelState.IsValid)
      return View(model);

    var entity = await _propertyService.GetEntity(model.Id);
    model.Agent = entity.Agent;
    model.Code = entity.Code;

    model.Portrait = ManageFile.UploadProperty(model.ImageFile, _currentUser.Id, model.Id, true, entity.Portrait);

    await _propertyService.Edit(model);

    if (model.UpgradeId != null) {
      var upgrades = await _upgradeService.GetAll();
      var validateUpgrades = upgrades.Where(x => model.UpgradeId.Contains(x.Id)).ToList();
      var propUpgrades = await _propUpgradeService.GetAll().ContinueWith(x => x.Result.Where(y => y.PropertyId == model.Id).ToList());

      List<SavePropUpgradeVm> propertyUpgrades = new();

      foreach (var upgrade in validateUpgrades)
      {
        propertyUpgrades.Add(new SavePropUpgradeVm()
        {
          PropertyId = model.Id,
          UpgradeId = upgrade.Id,
        });
      }
      propertyUpgrades = propertyUpgrades.Where(x => !propUpgrades.Select(y => y.UpgradeId).Contains(x.UpgradeId)).ToList();

      await _propUpgradeService.SaveRange(propertyUpgrades);
    }

    return RedirectToAction("OwnProperty");
  }

  public async Task<IActionResult> Delete(int id)
  {
    var property = await _propertyService.GetEntity(id);
    if (property != null)
      await _propertyService.Delete(id);

    return RedirectToAction("OwnProperty");
  }

  private async Task<SavePropertyVm> Error(SavePropertyVm model)
  {
    model.Types = await _propTypeService.GetAll();
    model.Sales = await _saleService.GetAll();
    return model;
  }

}
