
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;

namespace RSApp.Core.Services.Repositories;

public interface IPropUpgradeRepository : IGenericRepository<PropertyUpgrade> {
  Task SaveRange(IEnumerable<PropertyUpgrade> entities);
  Task DeleteRange(IEnumerable<PropertyUpgrade> entities);

  Task<IEnumerable<PropertyUpgrade>> GetByPropertyId(int id);
}
