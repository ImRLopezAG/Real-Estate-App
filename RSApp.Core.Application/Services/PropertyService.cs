using AutoMapper;
using Microsoft.AspNetCore.Http;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.Dtos.Account;
using RSApp.Core.Services.Helpers;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Services;

public class PropertyService : GenericService<PropertyVm, SavePropertyVm, Property>, IPropertyService
{
  private readonly IPropertyRepository _propertyRepository;
  private readonly IPropTypeRepository _propTypeRepository;
  private readonly ISaleRepository _saleRepository;
  private readonly IImageRepository _imageRepository;
  private readonly IPropUpgradeRepository _propUpgradeRepository;
  private readonly IUpgradeRepository _upgradeRepository;
  private readonly IFavoriteRepository _favoriteRepository;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IUserService _userService;
  private readonly IMapper _mapper;
  private readonly AuthenticationResponse? _currentUser;

  public PropertyService(IPropertyRepository propertyRepository, IMapper mapper, IPropTypeRepository propTypeRepository, ISaleRepository saleRepository, IImageRepository imageRepository, IPropUpgradeRepository propUpgradeRepository, IUpgradeRepository upgradeRepository, IUserService userService, IFavoriteRepository favoriteRepository, IHttpContextAccessor httpContextAccessor) : base(propertyRepository, mapper)
  {
    _propertyRepository = propertyRepository;
    _mapper = mapper;
    _propTypeRepository = propTypeRepository;
    _saleRepository = saleRepository;
    _imageRepository = imageRepository;
    _propUpgradeRepository = propUpgradeRepository;
    _upgradeRepository = upgradeRepository;
    _userService = userService;
    _favoriteRepository = favoriteRepository;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }

  public override async Task<IEnumerable<PropertyVm>> GetAll(){
    var types = await _propTypeRepository.GetAll();
    var sales = await _saleRepository.GetAll();
    var users = await _userService.GetAll();
    var favorites = await _favoriteRepository.GetAll();
    var upgrades = await _upgradeRepository.GetAll();
    var images = await _imageRepository.GetAll();
    var propUpgrades = await _propUpgradeRepository.GetAll();

    var query = from property in await _propertyRepository.GetAll()
                join type in types on property.TypeId equals type.Id
                join sale in sales on property.SaleId equals sale.Id
                join user in users on property.Agent equals user.Id
                select _mapper.Map<PropertyVm>(property, opt => opt.AfterMap((src, ppt) =>
                {
                  ppt.Type = type.Name;
                  ppt.Sale = sale.Name;
                  ppt.Seller = user;
                  ppt.Images = _mapper.Map<ICollection<ImageVm>>(images.Where(i => i.PropertyId == property.Id));
                  ppt.Upgrades = _mapper.Map<ICollection<UpgradeVm>>(upgrades.Where(u => propUpgrades.Any(pu => pu.PropertyId == property.Id && pu.UpgradeId == u.Id)));
                  ppt.Favorite = _currentUser != null ? favorites.Any(f => f.PropertyId == property.Id && f.UserId == _currentUser.Id) : false;
                }));

    return query;
  }

  public async override Task Delete(int id){
    var property = await _propertyRepository.GetEntity(id);

    var images = await _imageRepository.GetByPropertyId(id);
    var upgrades = await _propUpgradeRepository.GetByPropertyId(id);

    await _imageRepository.DeleteRange(images.ToList());
    await _propUpgradeRepository.DeleteRange(upgrades);
    await base.Delete(id);
  }
}
