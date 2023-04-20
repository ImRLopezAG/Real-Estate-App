using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RSApp.Presentation.WebApp.Controllers;
[Authorize(Roles = "Client")]
public class ClientController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}
