using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RSApp.Core.Application.Services;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.Services;
using System.Reflection;

namespace RSApp.Core.Application;

public static class ServiceRegistration{
  public static void AddApplicationServices(this IServiceCollection services){
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    #region Services
    services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
    services.AddTransient<IUserService, UserService>();
    services.AddTransient<IPropertyService, PropertyService>();
    services.AddTransient<IPropTypeService, PropTypeService>();
    services.AddTransient<ISaleService, SaleService>();
    services.AddTransient<IUpgradeService, UpgradeService>();
    services.AddTransient<IFavoriteService, FavoriteService>();
    services.AddTransient<IImageService, ImageService>();
    services.AddTransient<IPropUpgradeService, PropUpgradeService>();
    #endregion
  }
}
