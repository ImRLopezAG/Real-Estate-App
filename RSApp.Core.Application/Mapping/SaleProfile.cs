using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Dtos.Sale;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Mapping;

public class SaleProfile : Profile {
  public SaleProfile() {
    CreateMap<Sale, SaleVm>()
    .ForMember(vm => vm.HasError, opt => opt.Ignore())
    .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
    .ReverseMap();

    CreateMap<Sale, SaveSaleVm>()
    .ForMember(vm => vm.HasError, opt => opt.Ignore())
    .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
    .ReverseMap();

    CreateMap<Sale, SaleDto>()
    .ReverseMap()
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<Sale, SaveSaleDto>()
    .ReverseMap()
    .ForMember(ent => ent.Properties, opt => opt.Ignore());
  }
}
