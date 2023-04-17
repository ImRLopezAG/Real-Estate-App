using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;

namespace RSApp.Core.Services.Repositories;

public interface IImageRepository : IGenericRepository<Image> {
  Task DeleteRange(List<Image> images);
}