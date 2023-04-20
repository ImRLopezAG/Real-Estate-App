using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Services;
using RSApp.Presentation.WebApp.Models;
using System.Diagnostics;

namespace RSApp.Presentation.WebApp.Controllers;

public class HomeController : Controller {
  private readonly IUserService _userService;
  private readonly IPropertyService _propertyService;

  public HomeController(IUserService userService, IPropertyService propertyService) {
    _userService = userService;
    _propertyService = propertyService;
  }

  public IActionResult Index() {
    return View();
  }

  public IActionResult Privacy() {
    return View();
  }

  public async Task<IActionResult> Agents(){
    var agents = await _userService.GetAll().ContinueWith(t => t.Result.OrderBy(u => u.FirstName).ToList());
    return View(agents.Where(us => us.Role == "Agent" && us.EmailConfirmed == true));
  }

  public async Task<IActionResult> Properties(string id){
    var properties = await _propertyService.GetAll();
    return View(properties.Where(p => p.Agent == id));
  }

}
