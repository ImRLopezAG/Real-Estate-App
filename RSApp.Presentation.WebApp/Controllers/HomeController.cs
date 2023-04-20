using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Services;
using RSApp.Presentation.WebApp.Models;
using System.Diagnostics;

namespace RSApp.Presentation.WebApp.Controllers;

public class HomeController : Controller
{
  private readonly IUserService _userService;
  private readonly IPropertyService _propertyService;

  public HomeController(IUserService userService, IPropertyService propertyService){
    _userService = userService;
    _propertyService = propertyService;
  }

  public async Task<IActionResult> Index() => View(await _propertyService.GetAll());

  [Authorize(Roles = "Admin")]
  public IActionResult Admin() => View();

  public async Task<IActionResult> Agents()
  {
    var agents = await _userService.GetAll().ContinueWith(t => t.Result.OrderBy(u => u.FirstName).ToList());
    return View(agents.Where(us => us.Role == "Agent" && us.EmailConfirmed == true));
  }

  public async Task<IActionResult> Properties(string id)
  {
    var properties = await _propertyService.GetAll();
    return View(properties.Where(p => p.Agent == id));
  }

  public async Task<IActionResult> AgentFilter(string name)
  {
    var users = await _userService.GetAll();
    return View(users.Where(x => x.FullName.Contains(name)).ToList());
  }

  [HttpPost]
  public async Task<IActionResult> Filter(string? propertyCode, int? propTypeId, double minPrice, double maxPrice, int bathrooms, int roomsQuantity)
  {
    var properties = await _propertyService.GetAll();

    if (propertyCode != null)
      properties = properties.Where(p => p.Code.Contains(propertyCode)).ToList();
    if (propTypeId != null && propTypeId != 0)
      properties = properties.Where(p => p.TypeId == propTypeId).ToList();
    if (minPrice != 0)
      properties = properties.Where(p => p.Price >= minPrice).ToList();
    if (maxPrice != 0)
      properties = properties.Where(p => p.Price <= maxPrice).ToList();
    if (bathrooms != 0)
      properties = properties.Where(p => p.Bathrooms >= bathrooms).ToList();
    if (roomsQuantity != 0)
      properties = properties.Where(p => p.Rooms >= roomsQuantity).ToList();
    
    return View("Index", properties);
  }
}
