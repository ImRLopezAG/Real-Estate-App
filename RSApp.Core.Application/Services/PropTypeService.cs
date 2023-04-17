using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Services;

public class PropTypeService : GenericService<PropTypeVm, SavePropTypeVm, PropType>, IPropTypeService {
  private readonly IPropTypeRepository _propTypeRepository;
  private readonly IMapper _mapper;

  public PropTypeService(IPropTypeRepository propTypeRepository, IMapper mapper) : base(propTypeRepository, mapper) {
    _propTypeRepository = propTypeRepository;
    _mapper = mapper;
  }
}