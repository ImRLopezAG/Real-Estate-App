using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.Repositories;
using RSApp.Infrastructure.Persistence.Context;
using RSApp.Infrastructure.Persistence.Core;
using RSApp.Infrastructure.Persistence.Repositories;

namespace RSApp.Infrastructure.Persistence {
  public static class ServiceRegistration {
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration) {
      #region DbContext
      if (configuration.GetValue<bool>("UseInMemoryDatabase")) {
        services.AddDbContext<RSAppContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
      } else {
        services.AddDbContext<RSAppContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        m => m.MigrationsAssembly(typeof(RSAppContext).Assembly.FullName)));
      }
      #endregion
      #region Repositories
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddScoped<IImageRepository, ImageRepository>();
      services.AddScoped<IPropertyRepository, PropertyRepository>();
      services.AddScoped<ISaleRepository, SaleRepository>();
      services.AddScoped<IPropTypeRepository, PropTypeRepository>();
      services.AddScoped<IUpgradeRepository, UpgradeRepository>();
      services.AddScoped<IPropUpgradeRepository, PropUpgradeRepository>();
      services.AddScoped<IFavoriteRepository, FavoriteRepository>();
      #endregion
    }
  }
}