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

public class PropertyService : GenericService<PropertyVm, SavePropertyVm, Property>, IPropertyService {
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

  public PropertyService(IPropertyRepository propertyRepository, IMapper mapper, IPropTypeRepository propTypeRepository, ISaleRepository saleRepository, IImageRepository imageRepository, IPropUpgradeRepository propUpgradeRepository, IUpgradeRepository upgradeRepository, IUserService userService, IFavoriteRepository favoriteRepository, IHttpContextAccessor httpContextAccessor) : base(propertyRepository, mapper) {
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

  public override async Task<IEnumerable<PropertyVm>> GetAll() {
    var types = await _propTypeRepository.GetAll();
    var sales = await _saleRepository.GetAll();
    var users = await _userService.GetAll();
    var favorites = await _favoriteRepository.GetAll();

    var query = from property in await _propertyRepository.GetAll()
                join type in types on property.TypeId equals type.Id
                join sale in sales on property.SaleId equals sale.Id
                join user in users on property.Agent equals user.Id
                select _mapper.Map<PropertyVm>(property, opt => opt.AfterMap((src, ppt) => {
                  ppt.Type = type.Name;
                  ppt.Sale = sale.Name;
                  ppt.Seller = user.FullName;
                  ppt.Favorite = _currentUser != null ? favorites.Any(f => f.UserId == _currentUser.Id && f.PropertyId == property.Id) : false;
                }));

    return query;
  }

  public async Task<IEnumerable<PropertyVm>> GetFavorites() {
    var favorites = await _favoriteRepository.GetAll();
    var types = await _propTypeRepository.GetAll();
    var sales = await _saleRepository.GetAll();

    var query = from property in await _propertyRepository.GetAll()
                join type in types on property.TypeId equals type.Id
                join sale in sales on property.SaleId equals sale.Id
                join favorite in favorites on property.Id equals favorite.PropertyId
                where favorite.UserId == _currentUser.Id
                select _mapper.Map<PropertyVm>(property, opt => opt.AfterMap((src, ppt) => {
                  ppt.Type = type.Name;
                  ppt.Sale = sale.Name;
                  ppt.Favorite = true;
                }));

    return query;
  }

  public async Task<IEnumerable<PropertyVm>> GetOwnProperties() {
    var types = await _propTypeRepository.GetAll();
    var sales = await _saleRepository.GetAll();

    var query = from property in await _propertyRepository.GetAll()
                join type in types on property.TypeId equals type.Id
                join sale in sales on property.SaleId equals sale.Id
                where property.Agent == _currentUser.Id
                select _mapper.Map<PropertyVm>(property, opt => opt.AfterMap((src, ppt) => {
                  ppt.Type = type.Name;
                  ppt.Sale = sale.Name;
                }));

    return query;
  }
}
