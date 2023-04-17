using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Services;

namespace RSApp.Presentation.WebApp.Controllers;

public class AgentController : Controller {
  private readonly IPropertyService _propertyService;

  public AgentController(IPropertyService propertyService) {
    _propertyService = propertyService;
  }

  public IActionResult Index() => View();
  public IActionResult OwnProperty() => View();

}
