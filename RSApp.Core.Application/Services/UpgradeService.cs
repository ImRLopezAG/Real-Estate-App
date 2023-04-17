using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Services;

public class UpgradeService : GenericService<UpgradeVm, SaveUpgradeVm, Upgrade>, IUpgradeService {
  private readonly IUpgradeRepository _upgradeRepository;
  private readonly IMapper _mapper;

  public UpgradeService(IUpgradeRepository upgradeRepository, IMapper mapper) : base(upgradeRepository, mapper) {
    _upgradeRepository = upgradeRepository;
    _mapper = mapper;
  }
}