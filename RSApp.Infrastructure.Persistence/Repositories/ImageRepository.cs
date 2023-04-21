using Microsoft.EntityFrameworkCore;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using RSApp.Infrastructure.Persistence.Context;
using RSApp.Infrastructure.Persistence.Core;

namespace RSApp.Infrastructure.Persistence.Repositories;

public class ImageRepository : GenericRepository<Image>, IImageRepository {
  private readonly RSAppContext _context;

  public ImageRepository(RSAppContext context) : base(context) => _context = context;

  public async Task DeleteRange(List<Image> images) {
    _context.Images.RemoveRange(images);
    await _context.SaveChangesAsync();
  }

  public async Task<IEnumerable<Image>> GetByPropertyId(int id) => await _context.Images.Where(x => x.PropertyId == id).ToListAsync();
}