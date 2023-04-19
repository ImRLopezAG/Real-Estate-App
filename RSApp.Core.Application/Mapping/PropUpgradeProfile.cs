using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Dtos.Property;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Mapping;

public class PropUpgradeProfile: Profile
{
    public PropUpgradeProfile()
    {
        CreateMap<PropertyUpgrade, PropUpgradeVm>()
            .ForMember(vm => vm.HasError, opt => opt.Ignore())
            .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(ent => ent.Property, opt => opt.Ignore());

        CreateMap<PropertyUpgrade, SavePropUpgradeVm>()
            .ForMember(vm => vm.HasError, opt => opt.Ignore())
            .ForMember(vm => vm.ErrorMessage, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(ent => ent.Property, opt => opt.Ignore());
    }
}
