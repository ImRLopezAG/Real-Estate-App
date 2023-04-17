using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using RSApp.Infrastructure.Persistence.Context;
using RSApp.Infrastructure.Persistence.Core;

namespace RSApp.Infrastructure.Persistence.Repositories;

public class SaleRepository : GenericRepository<Sale>, ISaleRepository {
  private readonly RSAppContext _context;

  public SaleRepository(RSAppContext context) : base(context) => _context = context;
}
