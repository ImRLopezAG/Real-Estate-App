using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Dtos.Type;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Mapping;

public class PropTypeProfile : Profile {
  public PropTypeProfile() {
    CreateMap<PropType, PropTypeVm>()
    .ForMember(vm => vm.HasError, opt => opt.Ignore())
    .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
    .ReverseMap();

    CreateMap<PropType, SavePropTypeVm>()
    .ForMember(vm => vm.HasError, opt => opt.Ignore())
    .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
    .ReverseMap();

    CreateMap<PropType, PropTypeDto>()
    .ReverseMap();

    CreateMap<PropType, SavePropTypeDto>()
    .ReverseMap();


  }
}
