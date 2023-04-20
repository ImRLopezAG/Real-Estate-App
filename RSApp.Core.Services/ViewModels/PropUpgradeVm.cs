using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSApp.Core.Services.Core.Models;

namespace RSApp.Core.Services.ViewModels;

public class PropUpgradeVm: BaseVm
{
  public int PropertyId { get; set; }
  public int UpgradeId { get; set; }
}
