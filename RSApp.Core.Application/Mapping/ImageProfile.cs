using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Dtos.Image;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Mapping;

public class ImageProfile : Profile {
  public ImageProfile() {
    CreateMap<Image, ImageVm>()
        .ForMember(vm => vm.HasError, opt => opt.Ignore())
        .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
        .ReverseMap()
        .ForMember(ent => ent.Property, opt => opt.Ignore());

    CreateMap<Image, SaveImageVm>()
        .ForMember(vm => vm.HasError, opt => opt.Ignore())
        .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
        .ReverseMap()
        .ForMember(ent => ent.Property, opt => opt.Ignore());

    CreateMap<Image, ImageDto>()
        .ReverseMap()
        .ForMember(ent => ent.Property, opt => opt.Ignore());

    CreateMap<Image, SaveImageDto>()
        .ReverseMap()
        .ForMember(ent => ent.Property, opt => opt.Ignore());
  }
}
