using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Services;
using RSApp.Presentation.WebApp.Models;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class AdminListViewComponent : ViewComponent {
  private readonly IPropertyService _propertyService;
  private readonly IUserService _userService;

  public AdminListViewComponent(IPropertyService propertyService, IUserService userService) {
    _propertyService = propertyService;
    _userService = userService;
  }

  public async Task<IViewComponentResult> InvokeAsync() {
    var users = await _userService.GetAll();
    var properties = await _propertyService.GetAll();

    var model = new AdminVm() {
      Properties = properties.Count(),
      Developers = users.Count(u => u.Role == "Dev"),
      UnActiveDevelopers = users.Count(u => u.Role == "Dev" && !u.EmailConfirmed),
      Clients = users.Count(u => u.Role == "Client"),
      UnActiveClients = users.Count(u => u.Role == "Client" && !u.EmailConfirmed)
    };

    return View(model);
  }
}
