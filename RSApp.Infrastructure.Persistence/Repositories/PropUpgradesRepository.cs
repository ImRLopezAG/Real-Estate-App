using Microsoft.EntityFrameworkCore;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using RSApp.Infrastructure.Persistence.Context;
using RSApp.Infrastructure.Persistence.Core;

namespace RSApp.Infrastructure.Persistence.Repositories;

public class PropUpgradeRepository : GenericRepository<PropertyUpgrade>, IPropUpgradeRepository {
  private readonly RSAppContext _context;

  public PropUpgradeRepository(RSAppContext context) : base(context) => _context = context;

  public async Task SaveRange(IEnumerable<PropertyUpgrade> entities){
    await _context.AddRangeAsync(entities);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteRange(IEnumerable<PropertyUpgrade> entities){
    _context.RemoveRange(entities);
    await _context.SaveChangesAsync();
  }

  public async Task<IEnumerable<PropertyUpgrade>> GetByPropertyId(int id) => await _context.PropertyUpgrades.Where(x => x.PropertyId == id).ToListAsync();
}