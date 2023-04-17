using AutoMapper;
using RSApp.Core.Application.Features.PropTypes.Commands.Create;
using RSApp.Core.Application.Features.PropTypes.Commands.Update;
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
    .ReverseMap()
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<PropType, SavePropTypeVm>()
    .ForMember(vm => vm.HasError, opt => opt.Ignore())
    .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
    .ReverseMap()
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<PropType, PropTypeDto>()
    .ReverseMap()
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<PropType, SavePropTypeDto>()
    .ReverseMap()
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<PropType, UpdatePropTypeResponse>()
    .ReverseMap()
    .ForMember(ent => ent.CreatedAt, opt => opt.Ignore())
    .ForMember(ent => ent.LastModifiedAt, opt => opt.Ignore())
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<PropType, UpdatePropTypeCommand>()
    .ReverseMap()
    .ForMember(ent => ent.CreatedAt, opt => opt.Ignore())
    .ForMember(ent => ent.LastModifiedAt, opt => opt.Ignore())
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<PropType, CreatePropTypeCommand>()
    .ReverseMap()
    .ForMember(ent => ent.CreatedAt, opt => opt.Ignore())
    .ForMember(ent => ent.LastModifiedAt, opt => opt.Ignore())
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

  }
}
