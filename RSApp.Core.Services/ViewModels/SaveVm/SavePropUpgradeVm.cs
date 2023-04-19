using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSApp.Core.Services.Core.Models;

namespace RSApp.Core.Services.ViewModels.SaveVm;

public class SavePropUpgradeVm: BaseVm
{
  public int PropertyId { get; set; }
  public int UpgradeId { get; set; }
}
