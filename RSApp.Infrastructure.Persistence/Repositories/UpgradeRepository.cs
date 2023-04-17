using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using RSApp.Infrastructure.Persistence.Context;
using RSApp.Infrastructure.Persistence.Core;

namespace RSApp.Infrastructure.Persistence.Repositories;

public class UpgradeRepository : GenericRepository<Upgrade>, IUpgradeRepository {
  private readonly RSAppContext _context;

  public UpgradeRepository(RSAppContext context) : base(context) => _context = context;
}