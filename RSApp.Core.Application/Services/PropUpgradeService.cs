using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Services;

public class PropUpgradeService : GenericService<PropUpgradeVm, SavePropUpgradeVm, PropertyUpgrade>, IPropUpgradeService
{
  private readonly IPropUpgradeRepository _propUpgradeRepository;
  private readonly IMapper _mapper;
  public PropUpgradeService(IPropUpgradeRepository propUpgradeRepository, IMapper mapper) : base(propUpgradeRepository, mapper)
  {
    _propUpgradeRepository = propUpgradeRepository;
    _mapper = mapper;
  }

  public async Task SaveRange(IEnumerable<SavePropUpgradeVm> models) => await _propUpgradeRepository.SaveRange(_mapper.Map<IEnumerable<PropertyUpgrade>>(models));  
}