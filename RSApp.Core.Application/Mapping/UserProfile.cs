using AutoMapper;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.ViewModels.SaveVm;
using RSApp.Core.Services.ViewModels.User;

namespace RSApp.Core.Application.Mapping;

public class UserProfile : Profile {
  public UserProfile() {
    CreateMap<AuthenticationRequest, LoginVm>()
        .ForMember(model => model.HasError, opt => opt.Ignore())
        .ForMember(model => model.Error, opt => opt.Ignore())
        .ReverseMap();

    CreateMap<RegisterRequest, SaveUserVm>()
        .ForMember(model => model.HasError, opt => opt.Ignore())
        .ForMember(model => model.Error, opt => opt.Ignore())
        .ForMember(model => model.ImageFile, opt => opt.Ignore())
        .ReverseMap();

    CreateMap<ForgotPasswordRequest, ForgotPasswordVm>()
        .ForMember(model => model.HasError, opt => opt.Ignore())
        .ForMember(model => model.Error, opt => opt.Ignore())
        .ReverseMap();

    CreateMap<ResetPasswordRequest, ResetPasswordVm>()
        .ForMember(model => model.HasError, opt => opt.Ignore())
        .ForMember(model => model.Error, opt => opt.Ignore())
        .ReverseMap();
  }
}
