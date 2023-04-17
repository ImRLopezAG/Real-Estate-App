using AutoMapper;
using RSApp.Core.Application.Features.Sales.Commands.Update;
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
    .ReverseMap()
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<Sale, SaveSaleVm>()
    .ForMember(vm => vm.HasError, opt => opt.Ignore())
    .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
    .ReverseMap()
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<Sale, SaleDto>()
    .ReverseMap()
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<Sale, SaveSaleDto>()
    .ReverseMap()
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

    CreateMap<Sale, UpdateSaleResponse>()
    .ReverseMap()
    .ForMember(ent => ent.CreatedAt, opt => opt.Ignore())
    .ForMember(ent => ent.LastModifiedAt, opt => opt.Ignore())
    .ForMember(ent => ent.Properties, opt => opt.Ignore());

  }
}
