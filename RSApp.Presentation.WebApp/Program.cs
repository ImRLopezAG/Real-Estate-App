using Microsoft.AspNetCore.Identity;
using RSApp.Core.Application;
using RSApp.Infrastructure.Identity;
using RSApp.Infrastructure.Identity.Entities;
using RSApp.Infrastructure.Identity.Seeds;
using RSApp.Infrastructure.Persistence;
using RSApp.Infrastructure.Shared;
using RSApp.Presentation.WebApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddScoped<SaveAuthorize>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ValidateSessions, ValidateSessions>();

builder.Services.AddAuthorization(opt => {
  opt.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
  opt.AddPolicy("Agent", policy => policy.RequireRole("Agent"));
  opt.AddPolicy("Client", policy => policy.RequireRole("Client"));
  opt.AddPolicy("AdminOrAgent", policy => policy.RequireRole("Admin", "Agent"));
  opt.AddPolicy("AdminOrClient", policy => policy.RequireRole("Admin", "User"));
  opt.AddPolicy("AgentOrClient", policy => policy.RequireRole("Agent", "Client"));
});



var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
  var services = scope.ServiceProvider;

  try {
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DefaultRoles.SeedAsync(userManager, roleManager);
    await DefaultDevUser.SeedAsync(userManager, roleManager);
    await DefaultAdminUser.SeedAsync(userManager, roleManager);
  } catch {
    throw;
  }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
