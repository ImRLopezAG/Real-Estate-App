using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Services.Services;

public interface IFavoriteService : IGenericService<FavoriteVm, SaveFavoriteVm, Favorite> {
  Task<Favorite> GetByPropAndUser(int propId, string userId);
}
