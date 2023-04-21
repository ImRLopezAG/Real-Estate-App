using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Services.Services;

public interface IPropUpgradeService: IGenericService<PropUpgradeVm, SavePropUpgradeVm, PropertyUpgrade>
{
  Task SaveRange(IEnumerable<SavePropUpgradeVm> models);
}
