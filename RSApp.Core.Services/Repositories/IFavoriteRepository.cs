using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;

namespace RSApp.Core.Services.Repositories;

public interface IFavoriteRepository : IGenericRepository<Favorite> {
  Task<Favorite> GetByPropAndUser(int propId, string userId);

}
