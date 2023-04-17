using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Helpers;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels.SaveVm;
using RSApp.Presentation.WebApp.helpers;

namespace RSApp.Presentation.WebApp.Controllers;

public class PropertyController : Controller {
  private readonly IPropertyService _propertyService;
  private readonly IPropTypeService _propTypeService;
  private readonly ISaleService _saleService;
  private readonly IFavoriteService _favoriteService;
  private readonly IImageService _imageService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse _currentUser;

  public PropertyController(IPropertyService propertyService, IPropTypeService propTypeService, ISaleService saleService, IHttpContextAccessor httpContextAccessor, IImageService imageService, IFavoriteService favoriteService) {
    _propertyService = propertyService;
    _propTypeService = propTypeService;
    _saleService = saleService;
    _httpContextAccessor = httpContextAccessor;
    _imageService = imageService;
    _favoriteService = favoriteService;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }
  public IActionResult Index() => View();

  public async Task<IActionResult> Details(int id) => View(await _propertyService.GetById(id));

  public async Task<IActionResult> Create() => View(new SavePropertyVm() {
    Types = await _propTypeService.GetAll(),
    Sales = await _saleService.GetAll()
  });

  [HttpPost]
  public async Task<IActionResult> Create(SavePropertyVm model) {
    if (!ModelState.IsValid)
      return View(await Error(model));

    model.Agent = _currentUser.Id;
    model.Code = Guid.NewGuid().ToString()[..8].Replace("-", "").ToUpper();

    var created = await _propertyService.Create(model);
    if (created.Id != 0) {
      created.Portrait = ManageFile.UploadProperty(model.ImageFile, _currentUser.Id, created.Id);
      await _propertyService.Edit(created);

      if (model.ImageFiles != null) {
        foreach (var image in model.ImageFiles) {
          var img = new SaveImageVm() {
            PropertyId = created.Id,
            ImagePath = ManageFile.UploadPropertyImages(image, _currentUser.Id, created.Id)
          };
          await _imageService.Create(img);
        }
      }
    }

    return RedirectToAction("Index");
  }

  public async Task<IActionResult> Edit(int id) {
    var property = await _propertyService.GetEntity(id);
    property.Sales = await _saleService.GetAll();
    property.Types = await _propTypeService.GetAll();
    return View(property);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(SavePropertyVm model) {
    if (!ModelState.IsValid)
      return View(model);

    await _propertyService.Edit(model);
    return RedirectToAction("Index");
  }

  public async Task<IActionResult> Delete(int id) {
    var property = await _propertyService.GetEntity(id);
    if (property != null)
      await _propertyService.Delete(id);

    return RedirectToAction("Index");
  }

  [HttpPost]
  public async Task<IActionResult> AddToFavorite(int propertyId) {
    var url = Request.Headers["Referer"].ToString();
    var entity = await _propertyService.GetEntity(propertyId);
    if (entity != null) {
      var favorite = new SaveFavoriteVm() {
        PropertyId = propertyId,
        UserId = _currentUser.Id
      };
      await _favoriteService.Create(favorite);
    }
    // not redirect to previous page
    return RedirectToRoute(new { controller = url.Contains("Property") ? "Property" : "Home", action = "Index" });

  }

  [HttpPost]
  public async Task<IActionResult> DeleteFromFavorite(int propertyId) {
    var url = Request.Headers["Referer"].ToString();

    var entity = await _favoriteService.GetByPropAndUser(propertyId, _currentUser.Id);
    if (entity != null) {
      await _favoriteService.Delete(entity.Id);
    }
    return RedirectToRoute(new { controller = url.Contains("Property") ? "Property" : "Home", action = "Index" });
  }

  private async Task<SavePropertyVm> Error(SavePropertyVm model) {
    model.Types = await _propTypeService.GetAll();
    model.Sales = await _saleService.GetAll();
    return model;
  }
}
