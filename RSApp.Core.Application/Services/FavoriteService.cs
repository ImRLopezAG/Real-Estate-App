using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Services;

public class FavoriteService : GenericService<FavoriteVm, SaveFavoriteVm, Favorite>, IFavoriteService {
  private readonly IFavoriteRepository _favoriteRepository;
  private readonly IMapper _mapper;
  public FavoriteService(IFavoriteRepository repository, IMapper mapper) : base(repository, mapper) {
    _favoriteRepository = repository;
    _mapper = mapper;
  }

  public async Task<Favorite> GetByPropAndUser(int propId, string userId) => await _favoriteRepository.GetByPropAndUser(propId, userId);
}