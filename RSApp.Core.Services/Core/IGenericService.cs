using RSApp.Core.Domain.Core;
using RSApp.Core.Services.Core.Models;

namespace RSApp.Core.Services.Core;
public interface IGenericService<EntityVm, SaveEntityVm, Entity> : IBaseService<EntityVm> where EntityVm : Base where SaveEntityVm : Base where Entity : BaseEntity {
  Task<SaveEntityVm> GetEntity(int id);
  Task<SaveEntityVm> Create(SaveEntityVm vm);
  Task Edit(SaveEntityVm vm);
  Task Delete(int id);
}
