using Microsoft.AspNetCore.Mvc.Filters;
using RSApp.Presentation.WebApp.Controllers;

namespace RSApp.Presentation.WebApp.Middleware;

public class SaveAuthorize : IAsyncActionFilter {
  private readonly ValidateSessions _userSession;


  public SaveAuthorize(ValidateSessions userSession) {
    _userSession = userSession;
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
    if (!_userSession.IsAdmin()) {
      var controller = ( AdminUserController )context.Controller;
      context.Result = controller.RedirectToAction("Index", "Home");
    } else
      await next();
  }
}