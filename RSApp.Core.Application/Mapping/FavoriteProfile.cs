using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Dtos.Favorite;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Mapping;

public class FavoriteProfile : Profile {
  public FavoriteProfile() {
    CreateMap<Favorite, FavoriteVm>()
        .ForMember(vm => vm.HasError, opt => opt.Ignore())
        .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
        .ReverseMap()
        .ForMember(ent => ent.Property, opt => opt.Ignore());

    CreateMap<Favorite, SaveFavoriteVm>()
        .ForMember(vm => vm.HasError, opt => opt.Ignore())
        .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
        .ReverseMap()
        .ForMember(ent => ent.Property, opt => opt.Ignore());

    CreateMap<Favorite, FavoriteDto>()
        .ReverseMap()
        .ForMember(ent => ent.Property, opt => opt.Ignore());

    CreateMap<Favorite, SaveFavoriteDto>()
        .ReverseMap()
        .ForMember(ent => ent.Property, opt => opt.Ignore());
  }
}
