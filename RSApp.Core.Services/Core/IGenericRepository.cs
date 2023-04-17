using RSApp.Core.Domain.Core;
using System.Linq.Expressions;

namespace RSApp.Core.Services.Core;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity {
  Task<IEnumerable<TEntity>> GetAll();
  Task<TEntity> GetEntity(int id);
  Task<bool> Exists(Expression<Func<TEntity, bool>> Filter);
  Task<TEntity> Save(TEntity entity);
  Task Update(TEntity entity);
  Task Delete(TEntity entity);
}