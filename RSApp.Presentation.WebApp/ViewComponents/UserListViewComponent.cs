using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.Helpers;

namespace RSApp.Presentation.WebApp.ViewComponents;

public class UserListViewComponent : ViewComponent {
  private readonly IUserService _userService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse? _currentUser;

  public UserListViewComponent(IUserService userService, IHttpContextAccessor httpContextAccessor) {
    _userService = userService;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
  }

  public async Task<IViewComponentResult> InvokeAsync(string role) {
    
    var users = await _userService.GetAll().ContinueWith(x => x.Result.Where(us => us.Id != _currentUser.Id ));
    if (role != null)
      users = users.Where(us => us.Role == role);
    return View(users);
  }
}
