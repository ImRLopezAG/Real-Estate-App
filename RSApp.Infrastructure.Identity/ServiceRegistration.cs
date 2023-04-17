using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RSApp.Core.Domain.Settings;
using RSApp.Core.Services.Contracts;
using RSApp.Infrastructure.Identity.Entities;
using RSApp.Infrastructure.Identity.interfaces;
using RSApp.Infrastructure.Identity.Interfaces;
using RSApp.Infrastructure.Identity.Services;
using RSApp.Infrastructure.Persistence.Contexts;
using System.Text;

namespace RSApp.Infrastructure.Identity;

public static class ServiceRegistration {
  public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration) {

    #region Contexts
    ContextConfiguration(services, configuration);
    #endregion
    #region Identity
    IdentityConfiguration(services);
    #endregion
    services.AddAuthentication();
    #region Services
    ServiceConfiguration(services);
    #endregion
  }

  public static void AddIdentityInfrastructureForApi(this IServiceCollection services, IConfiguration configuration) {
    #region Contexts
    ContextConfiguration(services, configuration);
    #endregion

    #region Identity
    IdentityConfiguration(services);

    services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

    services.AddAuthentication(opt => {
      opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(opt => {
      opt.RequireHttpsMetadata = false;
      opt.SaveToken = true;
      opt.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = configuration["JWTSettings:Issuer"],
        ValidAudience = configuration["JWTSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
      };

      opt.Events = new JwtBearerEvents {
        OnAuthenticationFailed = context => {
          context.NoResult();
          context.Response.StatusCode = 500;
          context.Response.ContentType = "text/plain";
          return context.Response.WriteAsync(context.Exception.ToString());
        },
        OnChallenge = context => {
          context.HandleResponse();
          context.Response.StatusCode = 401;
          context.Response.ContentType = "application/json";
          var response = new {
            status = 401,
            message = "Unauthorized",
            hasError = true,
            error = "You are not authorized to access this resource"
          };
          return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        },
        OnForbidden = context => {
          context.Response.StatusCode = 403;
          context.Response.ContentType = "application/json";
          var response = new {
            status = 403,
            message = "Forbidden",
            hasError = true,
            error = "You are not authorized to access this resource"
          };
          return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

      };
    });
    #endregion

    #region Services
    ServiceConfiguration(services);
    #endregion
  }

  #region "Private methods"
  private static void ContextConfiguration(IServiceCollection services, IConfiguration configuration) {
    #region Contexts
    if (configuration.GetValue<bool>("UseInMemoryDatabase")) {
      services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
    } else {
      services.AddDbContext<IdentityContext>(options => {
        options.EnableSensitiveDataLogging();
        options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
      });
    }
    #endregion
  }

  private static void IdentityConfiguration(IServiceCollection services) {
    #region Identity
    services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

    services.ConfigureApplicationCookie(options => {
      options.LoginPath = "/User";
      options.AccessDeniedPath = "/User/AccessDenied";
    });
    #endregion
  }
  private static void ServiceConfiguration(IServiceCollection services) {
    #region Services
    services.AddTransient<IAccountService, AccountService>();
    services.AddTransient<IRequestService, RequestService>();
    services.AddTransient<IJwtService, JwtService>();
    #endregion
  }
  #endregion
}
