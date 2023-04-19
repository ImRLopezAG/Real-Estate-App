using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Dtos.Property;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Mapping;

public class PropertyProfile : Profile {
  public PropertyProfile() {
    CreateMap<Property, PropertyVm>()
      .ForMember(vm => vm.HasError, opt => opt.Ignore())
      .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
      .ForMember(vm => vm.Seller, opt => opt.Ignore())
      .ForMember(vm => vm.Type, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.Images, opt => opt.Ignore())
      .ForMember(ent => ent.PropertyUpgrades, opt => opt.Ignore())
      .ForMember(ent => ent.Type, opt => opt.Ignore())
      .ForMember(ent => ent.Sale, opt => opt.Ignore());

    CreateMap<Property, SavePropertyVm>()
      .ForMember(vm => vm.ImageFiles, opt => opt.Ignore())
      .ForMember(vm => vm.ImageFile, opt => opt.Ignore())
      .ForMember(vm => vm.Types, opt => opt.Ignore())
      .ForMember(vm => vm.Sales, opt => opt.Ignore())
      .ForMember(vm => vm.Upgrades, opt => opt.Ignore())
      .ForMember(vm => vm.HasError, opt => opt.Ignore())
      .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.Images, opt => opt.Ignore())
      .ForMember(ent => ent.PropertyUpgrades, opt => opt.Ignore())
      .ForMember(ent => ent.Type, opt => opt.Ignore())
      .ForMember(ent => ent.Sale, opt => opt.Ignore());

    CreateMap<Property, PropertyDto>()
      .ForMember(dto => dto.Portrait, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.Images, opt => opt.Ignore())
      .ForMember(ent => ent.PropertyUpgrades, opt => opt.Ignore())
      .ForMember(ent => ent.Type, opt => opt.Ignore())
      .ForMember(ent => ent.Sale, opt => opt.Ignore());

    CreateMap<Property, SavePropertyDto>()
      .ReverseMap()
      .ForMember(ent => ent.Images, opt => opt.Ignore())
      .ForMember(ent => ent.PropertyUpgrades, opt => opt.Ignore())
      .ForMember(ent => ent.Type, opt => opt.Ignore())
      .ForMember(ent => ent.Sale, opt => opt.Ignore());
  }
}
