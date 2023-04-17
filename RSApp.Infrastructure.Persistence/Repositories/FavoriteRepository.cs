using Microsoft.EntityFrameworkCore;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using RSApp.Infrastructure.Persistence.Context;
using RSApp.Infrastructure.Persistence.Core;

namespace RSApp.Infrastructure.Persistence.Repositories;

public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository {
  private readonly RSAppContext _context;

  public FavoriteRepository(RSAppContext context) : base(context) => _context = context;

  public async Task<Favorite> GetByPropAndUser(int propId, string userId) {
    return await _context.Favorites.FirstOrDefaultAsync(f => f.PropertyId == propId && f.UserId == userId);
  }
}
